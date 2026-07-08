using FluentValidation;
using Patients.Domain.ValueObjects;

namespace Patients.Application.Commands;

/// <summary>
/// Shape/format validation for the command (cheap, input-level checks). Domain invariants that
/// need aggregate state or the clock live on the aggregate, not here.
/// </summary>
public sealed class RegisterPatientValidator : AbstractValidator<RegisterPatientCommand>
{
    public RegisterPatientValidator()
    {
        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MaximumLength(FullName.MaxLength);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(FullName.MaxLength);

        RuleFor(command => command.DateOfBirth)
            .NotEmpty();
    }
}
