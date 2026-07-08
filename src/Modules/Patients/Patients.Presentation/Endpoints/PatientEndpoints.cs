using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Patients.Application.Commands;
using Patients.Application.Contracts;
using Patients.Presentation.Extensions;
using SharedKernel.Results;

namespace Patients.Presentation.Endpoints;

public static class PatientEndpoints
{
    public static IEndpointRouteBuilder MapPatientEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/patients").WithTags("Patients");

        group.MapPost("/", RegisterPatientAsync)
            .WithName("RegisterPatient")
            .Produces<PatientResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        return endpoints;
    }

    private static async Task<IResult> RegisterPatientAsync(
        RegisterPatientRequest request,
        RegisterPatientHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new RegisterPatientCommand(request.FirstName, request.LastName, request.DateOfBirth);

        Result<PatientResponse> result = await handler.HandleAsync(command, cancellationToken);

        return result.IsSuccess
            ? Results.Created($"/api/patients/{result.Value.Id}", result.Value)
            : result.Error.ToProblemResult();
    }
}

/// <summary>Transport contract for the register endpoint. Mapped by hand to the command.</summary>
public sealed record RegisterPatientRequest(string FirstName, string LastName, DateOnly DateOfBirth);
