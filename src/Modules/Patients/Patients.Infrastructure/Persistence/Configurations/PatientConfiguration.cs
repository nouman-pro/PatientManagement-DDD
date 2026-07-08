using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patients.Domain.Aggregates;
using Patients.Domain.ValueObjects;

namespace Patients.Infrastructure.Persistence.Configurations;

internal sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");

        builder.HasKey(patient => patient.Id);

        builder.Property(patient => patient.Id)
            .HasConversion(id => id.Value, value => new PatientId(value))
            .ValueGeneratedNever();

        builder.OwnsOne(patient => patient.Name, name =>
        {
            name.Property(fullName => fullName.First)
                .HasColumnName("FirstName")
                .HasMaxLength(FullName.MaxLength)
                .IsRequired();

            name.Property(fullName => fullName.Last)
                .HasColumnName("LastName")
                .HasMaxLength(FullName.MaxLength)
                .IsRequired();
        });

        builder.Navigation(patient => patient.Name).IsRequired();

        builder.Property(patient => patient.DateOfBirth).IsRequired();

        builder.Property(patient => patient.CreatedOnUtc).IsRequired();
        builder.Property(patient => patient.ModifiedOnUtc);

        // Domain events are never persisted; the outbox interceptor drains them on save.
        builder.Ignore(patient => patient.DomainEvents);
    }
}
