using Patients.Domain.Aggregates;

namespace Patients.Application.Contracts;

/// <summary>
/// Read model returned to callers. Mapping is hand-written (no AutoMapper) via
/// <see cref="FromDomain"/>, keeping the aggregate's shape out of the transport contract.
/// </summary>
public sealed record PatientResponse(Guid Id, string FirstName, string LastName, DateOnly DateOfBirth)
{
    public static PatientResponse FromDomain(Patient patient) => new(
        patient.Id.Value,
        patient.Name.First,
        patient.Name.Last,
        patient.DateOfBirth);
}
