#nullable disable warnings

using MediatR;
using Sales.Application.DTOs;
using Sales.Application.Shared;

namespace Sales.Application.Commands.Sales
{
    public class CreateSaleCommand : IRequest<Result<SaleDto>>
    {
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public IEnumerable<SaleItemCommand> Items { get; set; }
    }
}