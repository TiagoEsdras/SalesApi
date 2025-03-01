using MediatR;
using Sales.Application.DTOs;
using Sales.Application.Shared;

namespace Sales.Application.Queries.Products
{
    public class GetProductByIdQuery : IRequest<Result<ProductDto>>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}