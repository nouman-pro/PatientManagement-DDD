namespace Patients.Domain.Aggregates;

/// <summary>
/// Strongly-typed identity for <see cref="Patient"/>. A readonly record struct so it is
/// never accidentally interchangeable with another module's id — a clinical-safety concern.
/// </summary>
public readonly record struct PatientId(Guid Value)
{
    public static PatientId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}
