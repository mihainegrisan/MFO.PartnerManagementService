namespace MFO.PartnerManagementService.Application.Events;

public sealed record SellerRegisteredEvent(Guid PartnerId, string CompanyName);