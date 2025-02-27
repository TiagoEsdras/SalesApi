using MediatR;

namespace Sales.Application.Commands.Products
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
    }
}