using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands.Sales;
using Sales.Application.DTOs;
using Sales.Application.Queries.Sales;
using Sales.Application.Shared;
using SalesApi.Converters;

namespace SalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IActionResultConverter _actionResultConverter;

        public SalesController(IMediator mediator, IActionResultConverter actionResultConverter)
        {
            _mediator = mediator;
            _actionResultConverter = actionResultConverter;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<SaleDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result<SaleDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand request)
        {
            var result = await _mediator.Send(request);
            return _actionResultConverter.Convert(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Result<SaleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<SaleDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            var result = await _mediator.Send(new GetSaleByIdQuery(id));
            return _actionResultConverter.Convert(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<IEnumerable<SaleDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSales()
        {
            var result = await _mediator.Send(new GetSalesQuery());
            return _actionResultConverter.Convert(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelSale(Guid id)
        {
            var result = await _mediator.Send(new CancelSaleByIdQuery(id));
            return _actionResultConverter.Convert(result);
        }
    }
}