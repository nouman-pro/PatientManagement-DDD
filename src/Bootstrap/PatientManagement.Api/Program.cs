using PatientManagement.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddModules(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }))
    .WithName("HealthCheck")
    .WithTags("Diagnostics");

app.MapModuleEndpoints();

app.Run();

// Exposed so integration tests can spin up the host via WebApplicationFactory.
public partial class Program;
