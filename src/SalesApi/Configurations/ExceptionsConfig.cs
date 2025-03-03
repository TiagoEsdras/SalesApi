using SalesApi.ExceptionsHandler;

namespace SalesApi.Configurations
{
    public static class ExceptionsConfig
    {
        public static void AddExceptions(this IServiceCollection services)
        {
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
        }
    }
}