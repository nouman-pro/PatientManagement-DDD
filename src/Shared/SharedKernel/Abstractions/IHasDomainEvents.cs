namespace SharedKernel.Abstractions;

/// <summary>
/// Non-generic view of an aggregate's pending domain events. Lets infrastructure (e.g. the
/// outbox interceptor) collect events without knowing the aggregate's identity type.
/// </summary>
public interface IHasDomainEvents
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}
