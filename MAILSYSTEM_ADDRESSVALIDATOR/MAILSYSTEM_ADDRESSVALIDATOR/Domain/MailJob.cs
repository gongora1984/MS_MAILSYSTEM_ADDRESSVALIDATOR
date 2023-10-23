using MAILSYSTEM_ADDRESSVALIDATOR.Domain.Primitives;

namespace MAILSYSTEM_ADDRESSVALIDATOR.Domain;

public partial class MailJob : Entity, IAuditableEntity
{
    //// public Guid MailJobId { get; set; }

    public int MailJobId { get; set; }

    public Guid EnvelopTypeId { get; set; }

    public Guid CompanyId { get; set; }

    public string? KeyNumber { get; set; }

    public string LoanNumber { get; set; } = string.Empty;

    public int? Pages { get; set; }

    public int? Copies { get; set; }

    public decimal? TotalPostage { get; set; }

    public DateTime ReceivedOn { get; set; }

    public DateTime? VerifiedOn { get; set; }

    public DateTime? PrintedOn { get; set; }

    public DateTime? SendOutOn { get; set; }

    public string DocumentName { get; set; } = null!;

    public string DocumentType { get; set; } = null!;

    public string DocumentPath { get; set; } = null!;

    public string PropertyAddress1 { get; set; } = null!;

    public string? PropertyAddress2 { get; set; }

    public string PropertyCity { get; set; } = null!;

    public Guid PropertyState { get; set; }

    public string PropertyZip { get; set; } = null!;

    public bool? NeedManualCorrection { get; set; }

    public bool? Voided { get; set; }

    public DateTime? VoidedOn { get; set; }

    public string? VoidedNotes { get; set; }

    public string? CustomData1 { get; set; }

    public string? CustomData2 { get; set; }

    public string? CustomData3 { get; set; }

    public string? EnvelopeCaption1 { get; set; }

    public string? EnvelopeCaption2 { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ListItem EnvelopType { get; set; } = null!;

    public virtual ICollection<MailJobDetail> MailJobDetails { get; } = new List<MailJobDetail>();

    public virtual State MailJobPropertyStateNavigation { get; set; } = null!;
}
