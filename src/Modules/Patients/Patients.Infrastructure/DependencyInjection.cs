using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Patients.Application.Abstractions;
using Patients.Application.Commands;
using Patients.Domain.Abstractions;
using Patients.Infrastructure.Persistence;
using Patients.Infrastructure.Persistence.Repositories;
using Shared.Infrastructure.Auditing;
using Shared.Infrastructure.Messaging;

namespace Patients.Infrastructure;

public static class DependencyInjection
{
    private const string ConnectionStringName = "PatientManagement";

    public static IServiceCollection AddPatientsModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName)
            ?? throw new InvalidOperationException($"Connection string '{ConnectionStringName}' is not configured.");

        services.TryAddSingleton(TimeProvider.System);
        services.TryAddSingleton<AuditableEntityInterceptor>();
        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

        services.AddDbContext<PatientsDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(
                connectionString,
                sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", PatientsDbContext.Schema));

            options.AddInterceptors(
                serviceProvider.GetRequiredService<AuditableEntityInterceptor>(),
                serviceProvider.GetRequiredService<InsertOutboxMessagesInterceptor>());
        });

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<PatientsDbContext>());
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<RegisterPatientHandler>();
        services.AddValidatorsFromAssemblyContaining<RegisterPatientValidator>();

        return services;
    }
}
