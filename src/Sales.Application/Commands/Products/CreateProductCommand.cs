using MediatR;
using Sales.Application.Shared;
using Sales.Domain.Entities;

namespace Sales.Application.Commands.Products
{
    public class CreateProductCommand : IRequest<Result<Product>>
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
    }
}