using MFO.PartnerManagementService.Domain.Common;
using MFO.PartnerManagementService.Domain.Enums;

namespace MFO.PartnerManagementService.Domain.Entities;

/// <summary>
/// Represents the specific commercial persona or account that lists products and receives orders.
/// A single Partner can potentially own multiple Seller accounts (e.g., for different brands).
/// </summary>
public class Seller : AuditableEntity
{
    public required Guid PartnerId { get; set; }

    // Current operational status as a marketplace seller
    public required SellerStatus SellerStatus { get; set; }

    // Links to the Logistics Hub service (e.g., a reference ID for their primary fulfillment location)
    public required Guid DefaultWarehouseId { get; set; }

    // Links to the Pricing/Commission Configuration (e.g., "Standard Tier", "High Volume Tier")
    // This allows the Pricing Service to know the commission rate to charge this seller.
    // public required string CommissionProfileId { get; set; }

    // Rating and review aggregation (could be a denormalized value from the Review & Rating Service)
    // public required decimal AverageRating { get; set; } = 0.0m;


    public Partner? Partner { get; set; }
}