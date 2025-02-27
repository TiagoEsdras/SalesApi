using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands.Products;
using Sales.Application.Queries.Products;

namespace SalesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            var productId = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));

            if (product is null)
                return NotFound();

            return Ok(product);
        }
    }
}