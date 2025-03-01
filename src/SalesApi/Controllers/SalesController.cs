using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands.Sales;
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
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand request)
        {
            var result = await _mediator.Send(request);
            return _actionResultConverter.Convert(result);
        }
    }
}