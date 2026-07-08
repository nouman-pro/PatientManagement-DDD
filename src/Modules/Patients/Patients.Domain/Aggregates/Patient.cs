using Patients.Domain.Errors;
using Patients.Domain.Events;
using Patients.Domain.ValueObjects;
using SharedKernel.Abstractions;
using SharedKernel.Primitives;
using SharedKernel.Results;

namespace Patients.Domain.Aggregates;

/// <summary>
/// The Patients module's aggregate root. All mutation goes through behavior on this type;
/// construction is only possible via <see cref="Register"/>, which enforces invariants and
/// raises the corresponding domain event.
/// </summary>
public sealed class Patient : AggregateRoot<PatientId>, IAuditableEntity
{
    private Patient(PatientId id, FullName name, DateOnly dateOfBirth)
        : base(id)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
    }

    // Required by EF Core; properties are populated by the materializer.
    private Patient()
    {
    }

    public FullName Name { get; private set; } = null!;

    public DateOnly DateOfBirth { get; private set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    /// <summary>
    /// Registers a new patient. <paramref name="today"/> is passed in (rather than read from the
    /// clock) so the invariant stays deterministic and unit-testable.
    /// </summary>
    public static Result<Patient> Register(FullName name, DateOnly dateOfBirth, DateOnly today)
    {
        if (dateOfBirth > today)
        {
            return PatientErrors.DateOfBirthInFuture;
        }

        var patient = new Patient(PatientId.New(), name, dateOfBirth);
        patient.Raise(new PatientRegisteredDomainEvent(patient.Id, DateTime.UtcNow));

        return patient;
    }
}
