namespace SharedKernel.Abstractions;

/// <summary>
/// A contract published across module boundaries via the outbox. Unlike a domain event
/// it is part of a module's public API and must remain backwards compatible.
/// </summary>
public interface IIntegrationEvent
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}
