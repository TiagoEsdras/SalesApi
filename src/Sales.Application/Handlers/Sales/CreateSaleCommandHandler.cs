﻿using AutoMapper;
using MediatR;
using Sales.Application.Commands.Sales;
using Sales.Application.DTOs;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers.Sales
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Result<SaleDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public CreateSaleCommandHandler(IProductRepository productRepository, ISaleRepository saleRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<Result<SaleDto>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var products = request.Items.Select(it => it.ProductId).ToHashSet();
            var existingProducts = await _productRepository.GetByIdsAsync(products);

            if (products.Count != existingProducts.Count())
            {
                var notFoundProducts = products.Except(existingProducts.Select(it => it.Id));
                return Result<SaleDto>.NotFound(ErrorType.DataNotFound, string.Format(Consts.NotFoundEntity, nameof(Product)), string.Format(Consts.NotFoundEntityById, nameof(Product), string.Join(", ", notFoundProducts)));
            }

            var sale = _mapper.Map<Sale>(request);
            await _saleRepository.AddAsync(sale);
            var saleDto = _mapper.Map<SaleDto>(sale);
            return Result<SaleDto>.Persisted(saleDto, string.Format(Consts.EntityCreatedWithSuccess, nameof(Sale)));
        }
    }
}