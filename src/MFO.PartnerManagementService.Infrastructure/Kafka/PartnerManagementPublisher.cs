using MFO.PartnerManagementService.Application.Events;
using MFO.PartnerManagementService.Application.Interfaces;

namespace MFO.PartnerManagementService.Infrastructure.Kafka;

public class PartnerManagementPublisher : IPartnerManagementPublisher
{
    private const string SellerRegisteredTopic = "partner-management-seller-registered";
    private const string SellerStatusUpdatedTopic = "partner-management-seller-status-updated";
    private readonly IKafkaProducer _producer;

    public PartnerManagementPublisher(IKafkaProducer producer)
    {
        _producer = producer;
    }

    public async Task PublishSellerRegisteredEvent(SellerRegisteredEvent sellerRegisteredEvent)
    {
        var key = sellerRegisteredEvent.PartnerId;

        await _producer.ProduceAsync(
            SellerRegisteredTopic,
            key,
            sellerRegisteredEvent
        );

        Console.WriteLine($"Published Seller Registered Event for Company: '{sellerRegisteredEvent.CompanyName}'.");
    }

    public async Task PublishSellerStatusUpdatedEvent(SellerStatusUpdatedEvent sellerStatusUpdatedEvent)
    {
        var key = sellerStatusUpdatedEvent.PartnerId;

        await _producer.ProduceAsync(
            SellerStatusUpdatedTopic,
            key,
            sellerStatusUpdatedEvent
        );

        Console.WriteLine($"Published Seller Status Updated Event for PartnerId: '{sellerStatusUpdatedEvent.PartnerId}' with Status: '{sellerStatusUpdatedEvent.SellerStatus}'.");
    }
}