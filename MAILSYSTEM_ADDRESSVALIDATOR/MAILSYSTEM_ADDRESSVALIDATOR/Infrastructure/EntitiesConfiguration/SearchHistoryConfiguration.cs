using MAILSYSTEM_ADDRESSVALIDATOR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAILSYSTEM_ADDRESSVALIDATOR.Infrastructure;

internal sealed class SearchHistoryConfiguration : IEntityTypeConfiguration<SearchHistory>
{
    public void Configure(EntityTypeBuilder<SearchHistory> entity)
    {
        entity.ToTable(TableNames.SearchHistory);

        entity.HasKey(e => e.Id).HasName("PKSearchHistory");

        entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

        entity.HasIndex(e => e.CompanyId, "FKSearchHistoryCompanyCompanyId");

        entity.HasIndex(e => e.SearchTypeId, "FKSearchHistoryListItemSearchType");

        entity.Property(e => e.ReceivedOn).HasDefaultValueSql("(getdate())");

        entity.Property(e => e.Voided).HasDefaultValueSql("((0))");

        entity.Property(e => e.VoidedNotes)
            .HasMaxLength(1500)
            .IsUnicode(false);

        /*Relations*/
        entity.HasOne(d => d.Company).WithMany(p => p.SearchHistorys)
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FKSearchHistoryCompanyCompanyId");

        entity.HasOne(d => d.SearchType).WithMany(p => p.SearchHistorys)
            .HasForeignKey(d => d.SearchTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FKSearchHistoryListItemSearchType");
    }
}
