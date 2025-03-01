using MediatR;
using Sales.Application.Queries.Products;
using Sales.Application.Repositories;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers.Products
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<Product>>
    {
        private readonly IRepository<Product> _productRepository;

        public GetProductByIdQueryHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            
            if (product is null)
                return Result<Product>.NotFound(ErrorType.DataNotFound, string.Format(Consts.NotFoundEntity, nameof(Product)), string.Format(Consts.NotFoundEntityById, nameof(Product), request.Id));
            
            return Result<Product>.Success(product, string.Format(Consts.GetEntityByIdWithSuccess, nameof(Product)));
        }
    }
}