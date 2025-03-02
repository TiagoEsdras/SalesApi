using MediatR;
using Sales.Application.DTOs;
using Sales.Application.Shared;

namespace Sales.Application.Queries.Sales
{
    public class GetSalesQuery : IRequest<Result<IEnumerable<SaleDto>>>
    {
    }
}