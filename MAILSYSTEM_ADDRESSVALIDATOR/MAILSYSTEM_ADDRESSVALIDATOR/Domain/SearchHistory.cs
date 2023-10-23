using MAILSYSTEM_ADDRESSVALIDATOR.Domain.Primitives;

namespace MAILSYSTEM_ADDRESSVALIDATOR.Domain;

public partial class SearchHistory : Entity, IAuditableEntity
{
    public Guid SearchTypeId { get; set; }

    public Guid CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ListItem SearchType { get; set; } = null!;

    public DateTime ReceivedOn { get; set; }

    public DateTime? ProcessedOn { get; set; }

    public bool? Voided { get; set; }

    public DateTime? VoidedOn { get; set; }

    public string? VoidedNotes { get; set; }

    public virtual ICollection<SearchHistoryDetail> SearchHistoryDetails { get; } = new List<SearchHistoryDetail>();
}
