using AutoMapper;
using MediatR;
using Sales.Application.Commands.Products;
using Sales.Application.DTOs;
using Sales.Application.Repositories;
using Sales.Application.Shared;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers.Products
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.AddAsync(product);
            var productDto = _mapper.Map<ProductDto>(product);
            return Result<ProductDto>.Persisted(productDto, string.Format(Consts.EntityCreatedWithSuccess, nameof(Product)));
        }
    }
}