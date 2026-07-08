using Patients.Domain.Aggregates;
using SharedKernel.Abstractions;

namespace Patients.Domain.Events;

public sealed record PatientRegisteredDomainEvent(PatientId PatientId, DateTime OccurredOnUtc) : IDomainEvent;
