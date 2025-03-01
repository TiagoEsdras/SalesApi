using AutoMapper;
using FluentAssertions;
using Moq;
using Sales.Application.DTOs;
using Sales.Application.Handlers.Products;
using Sales.Application.Queries.Products;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Domain.Entities;
using Sales.Tests.Builders.DTOs;
using Xunit;

namespace Sales.Tests.Application.Handlers.Products
{
    public class GetProductsQueryHandlerTests
    {
        private readonly Mock<IRepository<Product>> _mockProductRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetProductsQueryHandler _handler;

        public GetProductsQueryHandlerTests()
        {
            _mockProductRepository = new Mock<IRepository<Product>>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetProductsQueryHandler(_mockProductRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenProductsAreFound()
        {
            // Arrange
            var query = new GetProductsQuery();

            var productsDto = new List<ProductDto>()
            {
                new ProductDtoBuilder().Build(),
                new ProductDtoBuilder().Build(),
                new ProductDtoBuilder().Build()
            };

            _mockProductRepository.Setup(r => r.GetAllAsync()).ReturnsAsync([new Product()]);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Product>>())).Returns(productsDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Data.Should().BeEquivalentTo(productsDto);
            result.Message.Should().Be(string.Format(Consts.GetEntitiesWithSuccess, nameof(Product)));
            _mockProductRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenNoProductsAreFound()
        {
            // Arrange
            var query = new GetProductsQuery();

            var emptyProductDtoList = new List<ProductDto>();

            _mockProductRepository.Setup(r => r.GetAllAsync()).ReturnsAsync([]);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Product>>())).Returns(emptyProductDtoList);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Data.Should().BeEquivalentTo(emptyProductDtoList);
            result.Message.Should().Be(string.Format(Consts.GetEntitiesWithSuccess, nameof(Product)));
            _mockProductRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}