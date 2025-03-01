using AutoMapper;
using MediatR;
using Sales.Application.Commands.Products;
using Sales.Application.Repositories;
using Sales.Application.Shared;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers.Products
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Product>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.AddAsync(product);
            return Result<Product>.Persisted(product, string.Format(Consts.EntityCreatedWithSuccess, nameof(Product)));
        }
    }
}