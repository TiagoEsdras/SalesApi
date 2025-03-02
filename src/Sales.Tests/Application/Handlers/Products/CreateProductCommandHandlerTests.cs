using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Sales.Application.DTOs;
using Sales.Application.Handlers.Products;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Application.Validators.Products;
using Sales.Domain.Entities;
using Sales.Tests.Builders.Commands;
using Sales.Tests.Builders.DTOs;
using Xunit;

namespace Sales.Tests.Application.Handlers.Products
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateProductCommandValidator _validator;
        private readonly CreateProductCommandHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new CreateProductCommandValidator();
            _handler = new CreateProductCommandHandler(_mockProductRepository.Object, _mockMapper.Object, _validator);
        }

        [Fact]
        public async Task Handle_ShouldReturnPersistedResult_WhenProductIsCreatedSuccessfully()
        {
            // Arrange
            var command = new CreateProductCommandBuilder().Build();

            var productDto = new ProductDtoBuilder()
                .WithId(Guid.NewGuid())
                .WithTitle(command.Title)
                .WithPrice(command.Price)
                .WithDescription(command.Description)
                .WithCategory(command.Category)
                .WithImage(command.Image)
                .Build();

            _mockMapper.Setup(m => m.Map<Product>(command)).Returns(new Product());
            _mockProductRepository.Setup(r => r.AddAsync(It.IsAny<Product>())).ReturnsAsync(new Product());
            _mockMapper.Setup(m => m.Map<ProductDto>(It.IsAny<Product>())).Returns(productDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.DataPersisted);
            result.Data.Should().BeEquivalentTo(productDto);
            result.Message.Should().Be(string.Format(Consts.EntityCreatedWithSuccess, nameof(Product)));
            _mockProductRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenValidationFails()
        {
            var command = new CreateProductCommandBuilder()
               .WithTitle("")
               .Build();

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
            _mockProductRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
        }
    }
}