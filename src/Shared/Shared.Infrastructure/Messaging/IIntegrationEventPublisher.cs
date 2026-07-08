using SharedKernel.Abstractions;

namespace Shared.Infrastructure.Messaging;

/// <summary>
/// Publishes integration events to other modules. The default implementation is expected
/// to write to the module's outbox so publication is transactional with the state change.
/// </summary>
public interface IIntegrationEventPublisher
{
    Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
}
