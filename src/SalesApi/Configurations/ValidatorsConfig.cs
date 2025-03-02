using FluentValidation;
using Sales.Application.Commands.Products;
using Sales.Application.Commands.Sales;
using Sales.Application.Queries.Products;
using Sales.Application.Queries.Sales;
using Sales.Application.Validators.Products;
using Sales.Application.Validators.Sales;
using Sales.Application.Validators.Shared;

namespace SalesApi.Configurations
{
    public static class ValidatorsConfig
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<SaleItemCommand>, SaleItemCommandValidator>();
            services.AddScoped<IValidator<CreateSaleCommand>, CreateSaleCommandValidator>();
            services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            services.AddScoped(typeof(IValidator<>), typeof(QueryByIdValidator<>));
            services.AddScoped<IValidator<CancelSaleByIdQuery>, QueryByIdValidator<CancelSaleByIdQuery>>();
            services.AddScoped<IValidator<GetSaleByIdQuery>, QueryByIdValidator<GetSaleByIdQuery>>();
            services.AddScoped<IValidator<GetProductByIdQuery>, QueryByIdValidator<GetProductByIdQuery>>();
        }
    }
}