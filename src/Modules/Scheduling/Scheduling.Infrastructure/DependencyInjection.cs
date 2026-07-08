using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Scheduling.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Wired-but-empty shell. Scheduling has no aggregates, persistence, or handlers yet;
    /// its DbContext and registrations land here when the module is carved out.
    /// </summary>
    public static IServiceCollection AddSchedulingModule(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        return services;
    }
}
