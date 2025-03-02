using AutoMapper;
using MediatR;
using Sales.Application.DTOs;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Queries.Sales;
using Sales.Application.Shared;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers.Sales
{
    public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, Result<IEnumerable<SaleDto>>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSalesQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<SaleDto>>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllAsync();
            var salesDto = _mapper.Map<IEnumerable<SaleDto>>(sales);
            return Result<IEnumerable<SaleDto>>.Success(salesDto, string.Format(Consts.GetEntitiesWithSuccess, nameof(Sale)));
        }
    }
}