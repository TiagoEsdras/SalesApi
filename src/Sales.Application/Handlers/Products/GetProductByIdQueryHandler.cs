using AutoMapper;
using MediatR;
using Sales.Application.DTOs;
using Sales.Application.Queries.Products;
using Sales.Application.Repositories;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers.Products
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product is null)
                return Result<ProductDto>.NotFound(ErrorType.DataNotFound, string.Format(Consts.NotFoundEntity, nameof(Product)), string.Format(Consts.NotFoundEntityById, nameof(Product), request.Id));

            var productDto = _mapper.Map<ProductDto>(product);
            return Result<ProductDto>.Success(productDto, string.Format(Consts.GetEntityByIdWithSuccess, nameof(Product)));
        }
    }
}