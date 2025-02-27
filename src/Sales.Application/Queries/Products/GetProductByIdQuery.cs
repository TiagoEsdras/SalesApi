using MediatR;
using Sales.Domain.Entities;

namespace Sales.Application.Queries.Products
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}