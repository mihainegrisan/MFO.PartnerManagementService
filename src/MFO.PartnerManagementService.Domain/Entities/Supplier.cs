using MFO.PartnerManagementService.Domain.Common;

namespace MFO.PartnerManagementService.Domain.Entities;

/// <summary>

/// </summary>
public class Supplier : AuditableEntity
{
    public required Guid PartnerId { get; set; }

    // Details specific to their supply chain role
    public required string PrimaryServiceProvided { get; set; } // e.g., "Logistics", "Raw Materials", "Fulfillment"

    // Defines their supply chain performance (e.g., 98% on-time delivery)
    public required decimal PerformanceScore { get; set; } = 1.0m;

    
    public Partner? Partner { get; set; }
}