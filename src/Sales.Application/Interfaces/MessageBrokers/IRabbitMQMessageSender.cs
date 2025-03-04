using Sales.Application.Events;

namespace Sales.Application.Interfaces.MessageBrokers
{
    public interface IRabbitMQMessageSender
    {
        Task SendMessage<T>(Event<T> baseMessage, string queueName);
    }
}