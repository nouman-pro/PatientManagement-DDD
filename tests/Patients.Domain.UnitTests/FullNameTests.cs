using Patients.Domain.Errors;
using Patients.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace Patients.Domain.UnitTests;

public sealed class FullNameTests
{
    [Fact]
    public void Create_WithValidNames_Succeeds()
    {
        var result = FullName.Create("Ada", "Lovelace");

        result.IsSuccess.ShouldBeTrue();
        result.Value.First.ShouldBe("Ada");
        result.Value.Last.ShouldBe("Lovelace");
    }

    [Theory]
    [InlineData("", "Lovelace")]
    [InlineData("   ", "Lovelace")]
    public void Create_WithMissingFirstName_ReturnsValidationError(string first, string last)
    {
        var result = FullName.Create(first, last);

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(PatientErrors.FirstNameRequired);
    }

    [Fact]
    public void Create_WithMissingLastName_ReturnsValidationError()
    {
        var result = FullName.Create("Ada", " ");

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(PatientErrors.LastNameRequired);
    }

    [Fact]
    public void Create_WithOverlongFirstName_ReturnsValidationError()
    {
        var result = FullName.Create(new string('a', FullName.MaxLength + 1), "Lovelace");

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(PatientErrors.FirstNameTooLong);
    }

    [Fact]
    public void Create_TrimsSurroundingWhitespace()
    {
        var result = FullName.Create("  Ada ", " Lovelace ");

        result.Value.First.ShouldBe("Ada");
        result.Value.Last.ShouldBe("Lovelace");
    }

    [Fact]
    public void Equality_IsByComponents()
    {
        var first = FullName.Create("Ada", "Lovelace").Value;
        var second = FullName.Create("Ada", "Lovelace").Value;

        first.ShouldBe(second);
        (first == second).ShouldBeTrue();
    }
}
