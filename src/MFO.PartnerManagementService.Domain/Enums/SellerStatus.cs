namespace MFO.PartnerManagementService.Domain.Enums;

public enum SellerStatus
{
    // Fully operational and allowed to create listings.
    Active = 1,
    // Temporarily suspended from selling (e.g., performance issues, late fees).
    Suspended = 2,
    // Account permanently closed.
    Closed = 3
}