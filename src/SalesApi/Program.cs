using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sales.Application.Commands.Products;
using Sales.Application.Handlers.Products;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Interfaces.Services;
using Sales.Application.Services;
using Sales.Application.Validators.Products;
using Sales.Infrastructure.Persistence;
using Sales.Infrastructure.Repositories;
using SalesApi.Converters;
using SalesApi.ExceptionsHandler;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    {
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SaleDbContext>(opt => opt.UseInMemoryDatabase("SaleDb"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

builder.Services.AddScoped<IActionResultConverter, ActionResultConverter>();

builder.Services.AddScoped<IDiscountCalculatorService, DiscountCalculatorService>();

builder.Services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();