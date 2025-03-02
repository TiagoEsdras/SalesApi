using AutoMapper;
using MediatR;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Queries.Sales;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers.Sales
{
    public class CancelSaleByIdQueryHandler : IRequestHandler<CancelSaleByIdQuery, Result<bool>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public CancelSaleByIdQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(CancelSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id);

            if (sale is null)
                return Result<bool>.NotFound(ErrorType.DataNotFound, string.Format(Consts.NotFoundEntity, nameof(Sale)), string.Format(Consts.NotFoundEntityById, nameof(Sale), request.Id));

            sale.Cancel();
            await _saleRepository.UpdateAsync(sale);

            return Result<bool>.Success(true, string.Format(Consts.SaleCanceledWithSuccess, request.Id));
        }
    }
}