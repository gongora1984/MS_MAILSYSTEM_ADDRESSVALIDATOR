using MAILSYSTEM_ADDRESSVALIDATOR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAILSYSTEM_ADDRESSVALIDATOR.Infrastructure;

internal sealed class SearchHistoryDetailConfiguration : IEntityTypeConfiguration<SearchHistoryDetail>
{
    public void Configure(EntityTypeBuilder<SearchHistoryDetail> entity)
    {
        entity.ToTable(TableNames.SearchHistoryDetails);

        entity.HasKey(e => e.Id).HasName("PKSearchHistoryDetails");

        entity.HasIndex(e => e.SearchHistoryId, "FKSearchHistorySearchHistoryDetail");

        entity.Property(e => e.Ssn)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.FirstName)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.MiddleName)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.Property(e => e.LastName)
            .HasMaxLength(150)
            .IsUnicode(false);

        entity.HasOne(d => d.SearchHistory).WithMany(p => p.SearchHistoryDetails)
            .HasForeignKey(d => d.SearchHistoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FKSearchHistorySearchHistoryDetail");
    }
}
