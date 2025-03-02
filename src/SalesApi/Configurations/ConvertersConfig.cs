using SalesApi.Converters;

namespace SalesApi.Configurations
{
    public static class ConvertersConfig
    {
        public static void AddConverters(this IServiceCollection services)
        {
            services.AddScoped<IActionResultConverter, ActionResultConverter>();
        }
    }
}