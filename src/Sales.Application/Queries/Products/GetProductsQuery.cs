using MediatR;
using Sales.Application.DTOs;
using Sales.Application.Shared;

namespace Sales.Application.Queries.Products
{
    public class GetProductsQuery : IRequest<Result<IEnumerable<ProductDto>>>
    {
        public GetProductsQuery()
        {
        }
    }
}