namespace MFO.PartnerManagementService.Application.Interfaces;

public interface IKafkaProducer
{
    Task ProduceAsync(string topic, object key, object value);
}