using MAILSYSTEM_ADDRESSVALIDATOR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAILSYSTEM_ADDRESSVALIDATOR.Infrastructure;

internal sealed class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> entity)
    {
        entity.HasKey(e => e.Id).HasName("PKState");

        entity.ToTable(TableNames.State);

        entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

        entity.Property(e => e.StateAbbreviation)
            .HasMaxLength(2)
            .IsUnicode(false);

        entity.Property(e => e.StateDescription)
            .HasMaxLength(25)
            .IsUnicode(false);
    }
}
