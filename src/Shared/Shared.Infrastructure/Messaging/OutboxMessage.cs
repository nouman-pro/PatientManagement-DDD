namespace Shared.Infrastructure.Messaging;

/// <summary>
/// A serialized integration event persisted in a module's schema, drained by a background
/// dispatcher. Each module owns its own outbox table — there are no cross-schema references.
/// </summary>
public sealed class OutboxMessage
{
    public Guid Id { get; init; }

    public required string Type { get; init; }

    public required string Content { get; init; }

    public DateTime OccurredOnUtc { get; init; }

    public DateTime? ProcessedOnUtc { get; set; }

    public string? Error { get; set; }
}
