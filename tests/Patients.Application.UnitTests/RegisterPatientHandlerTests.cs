using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using Patients.Application.Abstractions;
using Patients.Application.Commands;
using Patients.Domain.Abstractions;
using Patients.Domain.Aggregates;
using Shouldly;
using Xunit;

namespace Patients.Application.UnitTests;

public sealed class RegisterPatientHandlerTests
{
    private static readonly DateOnly Today = new(2026, 7, 8);

    private readonly IPatientRepository _repository = Substitute.For<IPatientRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IValidator<RegisterPatientCommand> _validator = Substitute.For<IValidator<RegisterPatientCommand>>();
    private readonly TimeProvider _timeProvider =
        new FixedTimeProvider(new DateTimeOffset(2026, 7, 8, 0, 0, 0, TimeSpan.Zero));

    private RegisterPatientHandler CreateHandler() =>
        new(_repository, _unitOfWork, _validator, _timeProvider);

    [Fact]
    public async Task HandleAsync_WhenValid_PersistsPatientAndReturnsResponse()
    {
        GivenValidationPasses();
        var command = new RegisterPatientCommand("Ada", "Lovelace", new DateOnly(1990, 1, 1));

        var result = await CreateHandler().HandleAsync(command, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();
        result.Value.FirstName.ShouldBe("Ada");
        result.Value.LastName.ShouldBe("Lovelace");
        _repository.Received(1).Add(Arg.Any<Patient>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_WhenValidationFails_ReturnsErrorAndDoesNotPersist()
    {
        var failure = new ValidationFailure("FirstName", "First name is required.")
        {
            ErrorCode = "Patient.FirstNameRequired",
        };
        _validator.ValidateAsync(Arg.Any<RegisterPatientCommand>(), Arg.Any<CancellationToken>())
            .Returns(new ValidationResult([failure]));
        var command = new RegisterPatientCommand(string.Empty, "Lovelace", new DateOnly(1990, 1, 1));

        var result = await CreateHandler().HandleAsync(command, CancellationToken.None);

        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe("Patient.FirstNameRequired");
        _repository.DidNotReceive().Add(Arg.Any<Patient>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_WhenDateOfBirthInFuture_ReturnsDomainErrorAndDoesNotPersist()
    {
        GivenValidationPasses();
        var command = new RegisterPatientCommand("Ada", "Lovelace", new DateOnly(2999, 1, 1));

        var result = await CreateHandler().HandleAsync(command, CancellationToken.None);

        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe("Patient.DateOfBirthInFuture");
        _repository.DidNotReceive().Add(Arg.Any<Patient>());
    }

    private void GivenValidationPasses() =>
        _validator.ValidateAsync(Arg.Any<RegisterPatientCommand>(), Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());

    private sealed class FixedTimeProvider(DateTimeOffset now) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => now;
    }
}
