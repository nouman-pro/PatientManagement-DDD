namespace Patients.Application.Abstractions;

/// <summary>
/// Commits the changes tracked within a single request as one transaction. Implemented by the
/// module's DbContext so the application layer never depends on EF Core directly.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
