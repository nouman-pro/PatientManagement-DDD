using Microsoft.EntityFrameworkCore;
using Patients.Application.Abstractions;
using Patients.Domain.Aggregates;
using Shared.Infrastructure.Messaging;

namespace Patients.Infrastructure.Persistence;

/// <summary>
/// The Patients module's single DbContext. Owns the <c>patients</c> schema and implements
/// <see cref="IUnitOfWork"/> so the application layer commits without referencing EF Core.
/// </summary>
public sealed class PatientsDbContext : DbContext, IUnitOfWork
{
    public const string Schema = "patients";

    public PatientsDbContext(DbContextOptions<PatientsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();

    internal DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PatientsDbContext).Assembly);
    }
}
