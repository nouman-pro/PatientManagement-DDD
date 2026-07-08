using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Patients.Infrastructure.Persistence;

/// <summary>
/// Design-time factory used by <c>dotnet ef</c> so migrations can be generated deterministically
/// without booting the host. The connection string here is never used to touch a database during
/// scaffolding — the model (and its <c>patients</c> schema) is all EF needs.
/// </summary>
internal sealed class PatientsDbContextFactory : IDesignTimeDbContextFactory<PatientsDbContext>
{
    public PatientsDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<PatientsDbContext>()
            .UseSqlServer(
                "Server=localhost;Database=PatientManagement;Trusted_Connection=True;TrustServerCertificate=True",
                sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", PatientsDbContext.Schema))
            .Options;

        return new PatientsDbContext(options);
    }
}
