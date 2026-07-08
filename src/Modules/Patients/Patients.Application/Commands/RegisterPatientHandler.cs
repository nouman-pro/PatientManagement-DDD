using FluentValidation;
using Patients.Application.Abstractions;
using Patients.Application.Contracts;
using Patients.Domain.Abstractions;
using Patients.Domain.Aggregates;
using Patients.Domain.ValueObjects;
using SharedKernel.Results;

namespace Patients.Application.Commands;

/// <summary>
/// Plain handler (no MediatR). Registered in DI and invoked directly by the endpoint.
/// Orchestrates validation, aggregate construction, persistence, and the unit of work.
/// </summary>
public sealed class RegisterPatientHandler
{
    private readonly IPatientRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<RegisterPatientCommand> _validator;
    private readonly TimeProvider _timeProvider;

    public RegisterPatientHandler(
        IPatientRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<RegisterPatientCommand> validator,
        TimeProvider timeProvider)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _timeProvider = timeProvider;
    }

    public async Task<Result<PatientResponse>> HandleAsync(
        RegisterPatientCommand command,
        CancellationToken cancellationToken = default)
    {
        var validation = await _validator.ValidateAsync(command, cancellationToken);
        if (!validation.IsValid)
        {
            var failure = validation.Errors[0];
            return Error.Validation(
                string.IsNullOrEmpty(failure.ErrorCode) ? "Patient.Validation" : failure.ErrorCode,
                failure.ErrorMessage);
        }

        var nameResult = FullName.Create(command.FirstName, command.LastName);
        if (nameResult.IsFailure)
        {
            return nameResult.Error;
        }

        var today = DateOnly.FromDateTime(_timeProvider.GetUtcNow().UtcDateTime);
        var patientResult = Patient.Register(nameResult.Value, command.DateOfBirth, today);
        if (patientResult.IsFailure)
        {
            return patientResult.Error;
        }

        var patient = patientResult.Value;
        _repository.Add(patient);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return PatientResponse.FromDomain(patient);
    }
}
