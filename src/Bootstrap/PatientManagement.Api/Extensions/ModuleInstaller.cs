using Billing.Infrastructure;
using Billing.Presentation.Endpoints;
using Patients.Infrastructure;
using Patients.Presentation.Endpoints;
using Scheduling.Infrastructure;
using Scheduling.Presentation.Endpoints;

namespace PatientManagement.Api.Extensions;

/// <summary>
/// Single place where the host composes the modules. Each module exposes exactly two seams:
/// a DI installer (Infrastructure) and an endpoint mapper (Presentation).
/// </summary>
public static class ModuleInstaller
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPatientsModule(configuration);
        services.AddSchedulingModule(configuration);
        services.AddBillingModule(configuration);
        return services;
    }

    public static IEndpointRouteBuilder MapModuleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPatientEndpoints();
        endpoints.MapSchedulingEndpoints();
        endpoints.MapBillingEndpoints();
        return endpoints;
    }
}
