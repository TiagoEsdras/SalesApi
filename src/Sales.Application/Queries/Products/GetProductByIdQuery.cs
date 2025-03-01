using MediatR;
using Sales.Application.Shared;
using Sales.Domain.Entities;

namespace Sales.Application.Queries.Products
{
    public class GetProductByIdQuery : IRequest<Result<Product>>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}