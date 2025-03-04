using Sales.Application.Interfaces.MessageBrokers;
using Sales.Infrastructure.MessageBroker;

namespace SalesApi.Configurations
{
    public static class MessageSenderConfig
    {
        public static void AddMessageSender(this IServiceCollection services)
        {
            services.AddScoped<IRabbitMQMessageSender, RabbitMQMessageSender>();
        }
    }
}