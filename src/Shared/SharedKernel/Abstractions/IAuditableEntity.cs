namespace SharedKernel.Abstractions;

/// <summary>
/// Marks an entity whose create/modify timestamps are stamped automatically on save.
/// Lives in the kernel (a domain-agnostic primitive) so aggregates can be auditable
/// without depending on infrastructure; the stamping mechanism lives in Shared.Infrastructure.
/// </summary>
public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; set; }

    DateTime? ModifiedOnUtc { get; set; }
}
