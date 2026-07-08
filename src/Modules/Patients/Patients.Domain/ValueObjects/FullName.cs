using Patients.Domain.Errors;
using SharedKernel.Primitives;
using SharedKernel.Results;

namespace Patients.Domain.ValueObjects;

/// <summary>
/// A patient's name as an immutable value object. Constructed only through <see cref="Create"/>
/// so it can never hold an invalid state.
/// </summary>
public sealed class FullName : ValueObject
{
    public const int MaxLength = 100;

    private FullName(string first, string last)
    {
        First = first;
        Last = last;
    }

    public string First { get; }

    public string Last { get; }

    public static Result<FullName> Create(string? first, string? last)
    {
        if (string.IsNullOrWhiteSpace(first))
        {
            return PatientErrors.FirstNameRequired;
        }

        if (string.IsNullOrWhiteSpace(last))
        {
            return PatientErrors.LastNameRequired;
        }

        first = first.Trim();
        last = last.Trim();

        if (first.Length > MaxLength)
        {
            return PatientErrors.FirstNameTooLong;
        }

        if (last.Length > MaxLength)
        {
            return PatientErrors.LastNameTooLong;
        }

        return new FullName(first, last);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return First;
        yield return Last;
    }
}
