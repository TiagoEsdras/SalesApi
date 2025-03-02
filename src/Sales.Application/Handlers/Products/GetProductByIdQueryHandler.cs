using AutoMapper;
using MediatR;
using Sales.Application.DTOs;
using Sales.Application.Queries.Products;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Domain.Entities;
using FluentValidation;

namespace Sales.Application.Handlers.Products
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetProductByIdQuery> _validator;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper, IValidator<GetProductByIdQuery> validator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product is null)
                return Result<ProductDto>.NotFound(ErrorType.DataNotFound, string.Format(Consts.NotFoundEntity, nameof(Product)), string.Format(Consts.NotFoundEntityById, nameof(Product), request.Id));

            var productDto = _mapper.Map<ProductDto>(product);
            return Result<ProductDto>.Success(productDto, string.Format(Consts.GetEntityByIdWithSuccess, nameof(Product)));
        }
    }
}