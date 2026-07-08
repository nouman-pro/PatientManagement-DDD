namespace Patients.Application.Commands;

public sealed record RegisterPatientCommand(string FirstName, string LastName, DateOnly DateOfBirth);
