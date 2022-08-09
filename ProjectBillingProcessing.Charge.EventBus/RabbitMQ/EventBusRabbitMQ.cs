using ProjectBillingProcessing.Charge.EventBus.EventBus;
using ProjectBillingProcessing.Charge.EventBus.EventBus.Events;
using System.Text.Json;

namespace ProjectBillingProcessing.Charge.EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        private readonly IRabbitMQConnect _rabbitMQConnect;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly int _retry;
        private const string exchangeName = "project_billingprocessing";
        public EventBusRabbitMQ(IRabbitMQConnect rabbitMQConnect, ILogger<EventBusRabbitMQ> logger, int retry = 3)
        {
            _rabbitMQConnect = rabbitMQConnect;
            _logger = logger;
            _retry = retry;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_rabbitMQConnect.IsConnected)
            {
                _rabbitMQConnect.TryConnect();
            }

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retry, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
                });

            var eventName = @event.GetType().Name;

            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

            using (var channel = _rabbitMQConnect.CreateModel())
            {
                _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);
                channel.ExchangeDeclare(exchange: exchangeName, type: "direct");
                var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;
                    _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);
                    channel.BasicPublish(
                        exchange: exchangeName,
                        routingKey: eventName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body);
                });
            }
        }
    }
}
