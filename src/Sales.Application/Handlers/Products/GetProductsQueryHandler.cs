using MediatR;
using Sales.Application.Queries.Products;
using Sales.Application.Repositories;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers.Products
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IRepository<Product> _productRepository;

        public GetProductsQueryHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync();
        }
    }
}