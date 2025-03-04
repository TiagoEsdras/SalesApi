using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Sales.Application.Commands.Sales;
using Sales.Application.DTOs;
using Sales.Application.Handlers.Sales;
using Sales.Application.Interfaces.MessageBrokers;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Application.Validators.Sales;
using Sales.Domain.Entities;
using Sales.Tests.Builders.Commands;
using Sales.Tests.Builders.DTOs;
using Sales.Tests.Builders.Entities;
using Xunit;

namespace Sales.Tests.Application.Handlers.Sales
{
    public class CreateSaleCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CreateSaleCommandValidator _validator;
        private readonly Mock<IValidator<SaleItemCommand>> _commandValidator;
        private readonly Mock<IRabbitMQMessageSender> _sender;
        private readonly CreateSaleCommandHandler _handler;

        public CreateSaleCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _mapperMock = new Mock<IMapper>();
            _commandValidator = new Mock<IValidator<SaleItemCommand>>();
            _validator = new CreateSaleCommandValidator(_commandValidator.Object);
            _sender = new Mock<IRabbitMQMessageSender>();
            _handler = new CreateSaleCommandHandler(_productRepositoryMock.Object, _saleRepositoryMock.Object, _mapperMock.Object, _validator, _sender.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPersistedResult_WhenSaleIsCreatedSuccessfully()
        {
            // Arrange
            var saleItems = new List<SaleItemCommand>() {
                new SaleItemCommandBuilder().Build(),
                new SaleItemCommandBuilder().Build()
            };

            var command = new CreateSaleCommandBuilder()
                .WithItems(saleItems)
                .Build();

            var saleDto = new SaleDtoBuilder().Build();

            _productRepositoryMock.Setup(r => r.GetByIdsAsync(It.IsAny<HashSet<Guid>>()))
                .ReturnsAsync(command.Items.Select(it => new ProductBuilder().WithId(it.ProductId).Build()));

            _mapperMock.Setup(m => m.Map<Sale>(It.IsAny<CreateSaleCommand>()))
                .Returns(new Sale());

            _saleRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Sale>()))
                .ReturnsAsync(new Sale());

            _mapperMock.Setup(m => m.Map<SaleDto>(It.IsAny<Sale>()))
                .Returns(saleDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Data.Should().BeEquivalentTo(saleDto);
            result.Status.Should().Be(ResultResponseKind.DataPersisted);
            result.Message.Should().Be(string.Format(Consts.EntityCreatedWithSuccess, nameof(Sale)));
            _productRepositoryMock.Verify(r => r.GetByIdsAsync(It.IsAny<HashSet<Guid>>()), Times.Once);
            _saleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_WhenSomeProductsAreMissing()
        {
            // Arrange
            var saleItems = new List<SaleItemCommand>() {
                new SaleItemCommandBuilder().Build(),
                new SaleItemCommandBuilder().Build()
            };

            var command = new CreateSaleCommandBuilder()
                .WithItems(saleItems)
                .Build();

            var existingProductIds = command.Items.Take(1).Select(it => it.ProductId);

            _productRepositoryMock.Setup(r => r.GetByIdsAsync(It.IsAny<HashSet<Guid>>()))
                .ReturnsAsync(existingProductIds.Select(id => new ProductBuilder().WithId(id).Build()));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Data.Should().BeNull();
            result.Status.Should().Be(ResultResponseKind.NotFound);
            result.ErrorType.Should().Be(ErrorType.DataNotFound);
            result.ErrorMessage.Should().Be(string.Format(Consts.NotFoundEntity, nameof(Product)));
            result.ErrorDetail.Should().Contain(string.Join(", ", command.Items.Select(it => it.ProductId).Except(existingProductIds)));
            _productRepositoryMock.Verify(r => r.GetByIdsAsync(It.IsAny<HashSet<Guid>>()), Times.Once);
            _saleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenValidationFails()
        {
            // Arrange
            var command = new CreateSaleCommandBuilder().
                WithSaleDate(default)
                .Build();

            // Act
            Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
            _productRepositoryMock.Verify(r => r.GetByIdsAsync(It.IsAny<HashSet<Guid>>()), Times.Never);
            _saleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Never);
        }
    }
}