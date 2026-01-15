using MFO.PartnerManagementService.Domain.Enums;

namespace MFO.PartnerManagementService.Application.Events;

public sealed record SellerStatusUpdatedEvent(Guid PartnerId, SellerStatus SellerStatus, DateTimeOffset Timestamp);