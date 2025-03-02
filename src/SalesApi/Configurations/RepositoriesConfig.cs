using Sales.Application.Interfaces.Repositories;
using Sales.Infrastructure.Repositories;

namespace SalesApi.Configurations
{
    public static class RepositoriesConfig
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
        }
    }
}