﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Sales.Application.DTOs;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Queries.Sales;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers.Sales
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, Result<SaleDto>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetSaleByIdQuery> _validator;

        public GetSaleByIdQueryHandler(ISaleRepository saleRepository, IMapper mapper, IValidator<GetSaleByIdQuery> validator)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<SaleDto>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            var sale = await _saleRepository.GetByIdAsync(request.Id);

            if (sale is null)
                return Result<SaleDto>.NotFound(ErrorType.DataNotFound, string.Format(Consts.NotFoundEntity, nameof(Sale)), string.Format(Consts.NotFoundEntityById, nameof(Sale), request.Id));

            var saleDto = _mapper.Map<SaleDto>(sale);
            return Result<SaleDto>.Success(saleDto, string.Format(Consts.GetEntityByIdWithSuccess, nameof(Sale)));
        }
    }
}