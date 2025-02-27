using MediatR;
using Sales.Application.Queries.Products;
using Sales.Application.Repositories;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers.Products
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IRepository<Product> _productRepository;

        public GetProductByIdQueryHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetByIdAsync(request.Id);
        }
    }
}