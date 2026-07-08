using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Patients.Infrastructure;

public static class DependencyInjection
{
    // Placeholder for the module-shell checkpoint. Replaced by the real registration
    // (DbContext, repository, handler) when the Patients vertical slice is built.
    public static IServiceCollection AddPatientsModule(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        return services;
    }
}
