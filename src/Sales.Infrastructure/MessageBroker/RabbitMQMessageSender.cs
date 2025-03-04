using RabbitMQ.Client;
using Sales.Application.Events;
using Sales.Application.Interfaces.MessageBrokers;
using System.Text;
using System.Text.Json;

namespace Sales.Infrastructure.MessageBroker
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender, IDisposable
    {
        private readonly IConnection _connection;
        private readonly JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = true };

        public RabbitMQMessageSender(IConnection connection)
        {
            _connection = connection;
        }

        public async Task SendMessage<T>(Event<T> baseMessage, string queueName)
        {
            using var channel = await _connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: queueName, false, false, false, arguments: null);
            byte[] body = GetMessageAsByteArray(baseMessage);
            await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);
        }

        private byte[] GetMessageAsByteArray<T>(Event<T> message)
        {
            var json = JsonSerializer.Serialize(message, jsonSerializerOptions);
            return Encoding.UTF8.GetBytes(json);
        }

        public void Dispose()
        {
            if (_connection != null && _connection.IsOpen)
            {
                _connection.CloseAsync().Wait();
                _connection.Dispose();
            }
        }
    }
}