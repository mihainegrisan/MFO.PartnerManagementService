using MFO.Contracts.Shared;

namespace MFO.PartnerManagementService.Domain.Common;

public class AuditableEntity : IAuditableEntity
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}