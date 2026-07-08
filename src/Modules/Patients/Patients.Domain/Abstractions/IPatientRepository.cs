using Patients.Domain.Aggregates;

namespace Patients.Domain.Abstractions;

/// <summary>
/// Persistence contract for the <see cref="Patient"/> aggregate. Defined in the Domain so the
/// domain owns its persistence shape; implemented in Infrastructure.
/// </summary>
public interface IPatientRepository
{
    void Add(Patient patient);

    Task<Patient?> GetByIdAsync(PatientId id, CancellationToken cancellationToken = default);
}
