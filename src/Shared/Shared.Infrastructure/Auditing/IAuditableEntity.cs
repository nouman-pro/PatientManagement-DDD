namespace Shared.Infrastructure.Auditing;

/// <summary>
/// Marks an entity whose create/modify timestamps are stamped automatically on save.
/// Combined with domain events this satisfies audit requirements without event sourcing.
/// </summary>
public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; set; }

    DateTime? ModifiedOnUtc { get; set; }
}
