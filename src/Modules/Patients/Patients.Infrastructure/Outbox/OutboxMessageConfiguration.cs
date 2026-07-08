using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.Messaging;

namespace Patients.Infrastructure.Outbox;

/// <summary>
/// Maps the shared <see cref="OutboxMessage"/> shape into this module's own schema. Each module
/// owns its outbox table — there are no cross-schema references.
/// </summary>
internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");

        builder.HasKey(message => message.Id);

        builder.Property(message => message.Id).ValueGeneratedNever();

        builder.Property(message => message.Type)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(message => message.Content).IsRequired();

        builder.Property(message => message.OccurredOnUtc).IsRequired();

        builder.HasIndex(message => message.ProcessedOnUtc);
    }
}
