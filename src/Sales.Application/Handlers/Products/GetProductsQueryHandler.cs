using MediatR;
using Sales.Application.Queries.Products;
using Sales.Application.Repositories;
using Sales.Application.Shared;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers.Products
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<IEnumerable<Product>>>
    {
        private readonly IRepository<Product> _productRepository;

        public GetProductsQueryHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<IEnumerable<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            return Result<IEnumerable<Product>>.Success(products, string.Format(Consts.GetEntitiesWithSuccess, nameof(Product)));
        }
    }
}