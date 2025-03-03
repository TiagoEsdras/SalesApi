using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Sales.Application.Handlers.Sales;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Application.Validators.Sales;
using Sales.Domain.Entities;
using Sales.Tests.Builders.Entities.Sales.Tests.Builders.Domain;
using Sales.Tests.Builders.Queries;
using Xunit;

namespace Sales.Tests.Application.Handlers.Sales
{
    public class CancelSaleByIdQueryHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CancelSaleByIdQueryValidator _validator;
        private readonly CancelSaleByIdQueryHandler _handler;

        public CancelSaleByIdQueryHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _mapperMock = new Mock<IMapper>();
            _validator = new CancelSaleByIdQueryValidator();
            _handler = new CancelSaleByIdQueryHandler(_saleRepositoryMock.Object, _mapperMock.Object, _validator);
        }

        [Fact]
        public async Task Handle_ShouldCancelSale_WhenSaleIsValid()
        {
            // Arrange
            var sale = new SaleBuilder()
                .Build();

            var query = new CancelSaleByIdQueryBuilder()
                .Build();

            _saleRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(sale);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Data.Should().BeTrue();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Message.Should().Be(string.Format(Consts.SaleCanceledWithSuccess, query.Id));
            _saleRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Sale>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_WhenSaleDoesNotExist()
        {
            // Arrange
            var query = new CancelSaleByIdQueryBuilder()
                .Build();

            _saleRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Sale)null!);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Data.Should().BeFalse();
            result.Status.Should().Be(ResultResponseKind.NotFound);
            result.ErrorType.Should().Be(ErrorType.DataNotFound);
            result.ErrorDetail.Should().Be(string.Format(Consts.NotFoundEntityById, nameof(Sale), query.Id));
        }

        [Fact]
        public async Task Handle_ShouldReturnBadRequest_WhenSaleIsAlreadyCancelled()
        {
            // Arrange
            var sale = new SaleBuilder()
                .WithIsCanceled(true)
                .Build();

            var query = new CancelSaleByIdQueryBuilder()
                .Build();

            _saleRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(sale);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Data.Should().BeFalse();
            result.Status.Should().Be(ResultResponseKind.BadRequest);
            result.ErrorType.Should().Be(ErrorType.InvalidOperation);
            result.ErrorDetail.Should().Be(string.Format(Consts.SaleHasAlreadyBeenCancelled, query.Id));
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenValidationFails()
        {
            // Arrange
            var query = new CancelSaleByIdQueryBuilder()
                .WithId(Guid.Empty)
                .Build();

            // Act
            Func<Task> act = () => _handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
            _saleRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}