using RabbitMQ.Client;

namespace SalesApi.Configurations
{
    public static class RabbitMqConnectionConfig
    {
        public static void AddRabbitMqConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMq:HostName"] ?? throw new ArgumentNullException("RabbitMq:HostName", "Host name cannot be null"),
                UserName = configuration["RabbitMq:UserName"] ?? throw new ArgumentNullException("RabbitMq:UserName", "User name cannot be null"),
                Password = configuration["RabbitMq:Password"] ?? throw new ArgumentNullException("RabbitMq:Password", "Password cannot be null"),
                Port = int.Parse(configuration["RabbitMq:Port"] ?? "5672")
            };

            services.AddScoped(sp => factory.CreateConnectionAsync().GetAwaiter().GetResult());
        }
    }
}