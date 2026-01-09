using Confluent.Kafka;
using MFO.PartnerManagementService.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MFO.PartnerManagementService.Infrastructure.Kafka;

public class KafkaProducer : IKafkaProducer, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;

    public KafkaProducer(ProducerConfig config, ILogger<KafkaProducer> logger)
    {
        _logger = logger;
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task ProduceAsync(string topic, object key, object value)
    {
        string serializedKey = key.ToString();
        string serializedValue = JsonSerializer.Serialize(value);

        var message = new Message<string, string>
        {
            Key = serializedKey,
            Value = serializedValue
        };

        try
        {
            var deliveryResult = await _producer.ProduceAsync(topic, message);

            if (deliveryResult.Status == PersistenceStatus.Persisted)
            {
                _logger.LogInformation(
                    "Produced message successfully. Topic={Topic} Partition={Partition} Offset={Offset} Key={Key}",
                    topic, deliveryResult.Partition.Value, deliveryResult.Offset.Value, serializedKey);
            }
            else
            {
                // This path is usually not hit, as ProduceAsync often throws an exception 
                // before returning a non-persisted status, but it's a good safeguard.
                _logger.LogWarning(
                    "Message produced but not fully persisted. Status: {Status}",
                    deliveryResult.Status);
            }
        }
        catch (ProduceException<string, string> e)
        {
            // 3. Handle exceptions thrown by the producer (e.g., broker unreachable, timeout)
            _logger.LogError(e, "Redpanda delivery failed for key {Key}: {Reason}", serializedKey, e.Error.Reason);
            // Re-throw to allow the Command Handler to potentially fail or retry the entire command/transaction.
            throw;
        }
    }

    public void Dispose()
    {
        // Crucial: Flush any messages pending delivery on application shutdown
        // This ensures pending messages are sent before the process terminates.
        _producer.Flush(TimeSpan.FromSeconds(10));
        _producer.Dispose();
    }
}