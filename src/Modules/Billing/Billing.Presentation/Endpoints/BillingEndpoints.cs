using Microsoft.AspNetCore.Routing;

namespace Billing.Presentation.Endpoints;

public static class BillingEndpoints
{
    /// <summary>
    /// Wired-but-empty shell — no routes yet. The host still calls this so the module is
    /// discoverable and gains a mapping point the day it grows endpoints.
    /// </summary>
    public static IEndpointRouteBuilder MapBillingEndpoints(this IEndpointRouteBuilder endpoints) => endpoints;
}
