using Patients.Domain.ValueObjects;
using SharedKernel.Results;

namespace Patients.Domain.Errors;

/// <summary>
/// Catalog of expected, domain-level failures for the Patients module. Codes are stable and
/// safe to surface to clients / map to problem responses.
/// </summary>
public static class PatientErrors
{
    public static readonly Error FirstNameRequired =
        Error.Validation("Patient.FirstNameRequired", "First name is required.");

    public static readonly Error LastNameRequired =
        Error.Validation("Patient.LastNameRequired", "Last name is required.");

    public static readonly Error FirstNameTooLong =
        Error.Validation("Patient.FirstNameTooLong", $"First name must be at most {FullName.MaxLength} characters.");

    public static readonly Error LastNameTooLong =
        Error.Validation("Patient.LastNameTooLong", $"Last name must be at most {FullName.MaxLength} characters.");

    public static readonly Error DateOfBirthInFuture =
        Error.Validation("Patient.DateOfBirthInFuture", "Date of birth cannot be in the future.");

    public static readonly Error NotFound =
        Error.NotFound("Patient.NotFound", "The requested patient was not found.");
}
