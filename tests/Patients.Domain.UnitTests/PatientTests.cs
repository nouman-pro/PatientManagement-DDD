using Patients.Domain.Aggregates;
using Patients.Domain.Errors;
using Patients.Domain.Events;
using Patients.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace Patients.Domain.UnitTests;

public sealed class PatientTests
{
    private static readonly DateOnly Today = new(2026, 7, 8);

    private static FullName ValidName() => FullName.Create("Ada", "Lovelace").Value;

    [Fact]
    public void Register_WithValidData_ReturnsPatientWithIdentityAndName()
    {
        var name = ValidName();

        var result = Patient.Register(name, new DateOnly(1990, 1, 1), Today);

        result.IsSuccess.ShouldBeTrue();
        result.Value.Id.Value.ShouldNotBe(Guid.Empty);
        result.Value.Name.ShouldBe(name);
    }

    [Fact]
    public void Register_WithValidData_RaisesPatientRegisteredDomainEvent()
    {
        var patient = Patient.Register(ValidName(), new DateOnly(1990, 1, 1), Today).Value;

        var domainEvent = patient.DomainEvents.ShouldHaveSingleItem().ShouldBeOfType<PatientRegisteredDomainEvent>();
        domainEvent.PatientId.ShouldBe(patient.Id);
    }

    [Fact]
    public void Register_WithFutureDateOfBirth_ReturnsError()
    {
        var result = Patient.Register(ValidName(), Today.AddDays(1), Today);

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(PatientErrors.DateOfBirthInFuture);
    }

    [Fact]
    public void Register_OnTheBirthDay_IsAllowed()
    {
        var result = Patient.Register(ValidName(), Today, Today);

        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public void ClearDomainEvents_RemovesPendingEvents()
    {
        var patient = Patient.Register(ValidName(), new DateOnly(1990, 1, 1), Today).Value;

        patient.ClearDomainEvents();

        patient.DomainEvents.ShouldBeEmpty();
    }
}
