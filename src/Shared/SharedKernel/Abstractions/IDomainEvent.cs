namespace SharedKernel.Abstractions;

/// <summary>
/// A fact that happened inside an aggregate boundary. Handled in-process, within the
/// same transaction as the change that produced it.
/// </summary>
public interface IDomainEvent
{
    DateTime OccurredOnUtc { get; }
}
