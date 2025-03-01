using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands.Products;
using Sales.Application.DTOs;
using Sales.Application.Queries.Products;
using Sales.Application.Shared;
using SalesApi.Converters;

namespace SalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IActionResultConverter _actionResultConverter;

        public ProductsController(IMediator mediator, IActionResultConverter actionResultConverter)
        {
            _mediator = mediator;
            _actionResultConverter = actionResultConverter;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            var result = await _mediator.Send(request);
            return _actionResultConverter.Convert(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return _actionResultConverter.Convert(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Result<ProductDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _mediator.Send(new GetProductsQuery());
            return _actionResultConverter.Convert(result);
        }
    }
}