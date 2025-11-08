namespace MFO.PartnerManagementService.Domain.Enums;

public enum PartnerType
{
    // A partner who sells goods to the end consumer (e.g., Nike, a local shop).
    Seller = 1,
    // A partner who provides goods/services to the marketplace operator or a seller
    // (e.g., a packaging company, a raw material provider).
    Supplier = 2,
    // A partner that is both a Seller and a Supplier.
    SellerAndSupplier = 3
}