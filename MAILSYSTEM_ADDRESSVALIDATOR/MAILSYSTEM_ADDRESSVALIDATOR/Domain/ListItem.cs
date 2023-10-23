using MAILSYSTEM_ADDRESSVALIDATOR.Domain.Primitives;

namespace MAILSYSTEM_ADDRESSVALIDATOR.Domain;

public partial class ListItem : Entity, IAuditableEntity
{
    //// public Guid ListItemid { get; set; }

    public string SystemCategory { get; set; } = null!;

    public string SystemTag { get; set; } = null!;

    public string ListItemDisplayText { get; set; } = null!;

    public int ListItemOrder { get; set; }

    public bool? ListItemEnable { get; set; }

    public virtual ICollection<MailJob> MailJobs { get; } = new List<MailJob>();
    public virtual ICollection<SearchHistory> SearchHistorys { get; } = new List<SearchHistory>();
}
