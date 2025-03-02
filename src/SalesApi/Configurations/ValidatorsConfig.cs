using FluentValidation;
using Sales.Application.Commands.Products;
using Sales.Application.Commands.Sales;
using Sales.Application.Validators.Products;
using Sales.Application.Validators.Sales;

namespace SalesApi.Configurations
{
    public static class ValidatorsConfig
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<SaleItemCommand>, SaleItemCommandValidator>();
            services.AddScoped<IValidator<CreateSaleCommand>, CreateSaleCommandValidator>();
            services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
        }
    }
}