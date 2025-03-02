using MediatR;
using Sales.Application.DTOs;
using Sales.Application.Shared;

namespace Sales.Application.Queries.Sales
{
    public class GetSaleByIdQuery : IRequest<Result<SaleDto>>, IQueryById
    {
        public Guid Id { get; set; }

        public GetSaleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}