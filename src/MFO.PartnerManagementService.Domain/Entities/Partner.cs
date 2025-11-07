using MFO.PartnerManagementService.Domain.Common;
using MFO.PartnerManagementService.Domain.Enums;

namespace MFO.PartnerManagementService.Domain.Entities;

/// <summary>
/// This is the core entity, representing the legal business or organization that is an official participant in the marketplace
/// (e.g., the owner of a seller account or supplier).
/// </summary>
public class Partner : AuditableEntity
{
    public required Guid PartnerId { get; set; }
    public required string LegalName { get; set; }
    public required string DisplayName { get; set; }
    public required PartnerType PartnerType { get; set; }


    // --- External Lookups (Keys from other services) ---

    // Links to TaxService to find tax registration details
    // public required string TaxId { get; set; }

    // Links to Localization Service (ISO 3166-1 alpha-2, e.g., "US", "DE")
    // public required string LegalCountryCode { get; set; }

    // FK to User Service - the primary admin/account owner.
    // public required Guid MainContactUserId { get; set; }


    // --- Operational Status ---

    public required OnboardingStatus OnboardingStatus { get; set; }

    // Verification status (e.g., "DOCUMENTS_VERIFIED", "BANK_ACCOUNT_VALID")
    public required bool IsLegalVerified { get; set; } = false;


    public Guid? SellerId { get; set; }
    public Seller? Seller { get; set; }

    public Guid? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
}
