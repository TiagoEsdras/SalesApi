using MediatR;
using Sales.Application.Shared;

namespace Sales.Application.Queries.Sales
{
    public class CancelSaleByIdQuery : IRequest<Result<bool>>, IQueryById
    {
        public Guid Id { get; set; }

        public CancelSaleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}