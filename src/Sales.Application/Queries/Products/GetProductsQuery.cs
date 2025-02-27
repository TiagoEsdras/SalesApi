using MediatR;
using Sales.Domain.Entities;

namespace Sales.Application.Queries.Products
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public GetProductsQuery()
        {
        }
    }
}