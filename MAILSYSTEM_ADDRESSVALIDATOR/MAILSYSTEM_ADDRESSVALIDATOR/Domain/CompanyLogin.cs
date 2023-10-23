using MAILSYSTEM_ADDRESSVALIDATOR.Domain.Primitives;

namespace MAILSYSTEM_ADDRESSVALIDATOR.Domain;

public partial class CompanyLogin : Entity, IAuditableEntity
{
    //// public Guid CompanyLoginId { get; set; }

    public Guid CompanyId { get; set; }

    public string? CompanyAccessToken { get; set; }

    public DateTime? CompanyLastAccess { get; set; }
    public DateTime CompanyAccessTokenValidTo { get; set; }

    public virtual Company Company { get; set; } = null!;
}
