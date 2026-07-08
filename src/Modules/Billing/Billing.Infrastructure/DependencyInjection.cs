using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Billing.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Wired-but-empty shell. Billing has no aggregates, persistence, or handlers yet;
    /// its DbContext and registrations land here when the module is carved out.
    /// </summary>
    public static IServiceCollection AddBillingModule(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        return services;
    }
}
