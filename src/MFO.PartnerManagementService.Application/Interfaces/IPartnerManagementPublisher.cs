using MFO.PartnerManagementService.Application.Events;

namespace MFO.PartnerManagementService.Application.Interfaces;

public interface IPartnerManagementPublisher
{
    Task PublishSellerRegisteredEvent(SellerRegisteredEvent sellerRegisteredEvent);
    Task PublishSellerStatusUpdatedEvent(SellerStatusUpdatedEvent sellerStatusUpdatedEvent);
}