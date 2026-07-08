using Microsoft.AspNetCore.Routing;

namespace Scheduling.Presentation.Endpoints;

public static class SchedulingEndpoints
{
    /// <summary>
    /// Wired-but-empty shell — no routes yet. The host still calls this so the module is
    /// discoverable and gains a mapping point the day it grows endpoints.
    /// </summary>
    public static IEndpointRouteBuilder MapSchedulingEndpoints(this IEndpointRouteBuilder endpoints) => endpoints;
}
