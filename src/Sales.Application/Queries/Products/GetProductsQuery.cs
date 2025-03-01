using MediatR;
using Sales.Application.Shared;
using Sales.Domain.Entities;

namespace Sales.Application.Queries.Products
{
    public class GetProductsQuery : IRequest<Result<IEnumerable<Product>>>
    {
        public GetProductsQuery()
        {
        }
    }
}