using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Sales.Application.DTOs;
using Sales.Application.Handlers.Sales;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Application.Validators.Sales;
using Sales.Domain.Entities;
using Sales.Tests.Builders.DTOs;
using Sales.Tests.Builders.Queries;
using Xunit;

namespace Sales.Tests.Application.Handlers.Sales
{
    public class GetSaleByIdQueryHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetSaleByIdQueryValidator _validator;
        private readonly GetSaleByIdQueryHandler _handler;

        public GetSaleByIdQueryHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _mapperMock = new Mock<IMapper>();
            _validator = new GetSaleByIdQueryValidator();
            _handler = new GetSaleByIdQueryHandler(_saleRepositoryMock.Object, _mapperMock.Object, _validator);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSaleIsFound()
        {
            // Arrange
            var query = new GetSaleByIdQueryBuilder()
                .Build();

            var saleDto = new SaleDtoBuilder()
                .Build();

            _saleRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Sale());

            _mapperMock.Setup(x => x.Map<SaleDto>(It.IsAny<Sale>()))
                .Returns(saleDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Data.Should().Be(saleDto);
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Message.Should().Be(string.Format(Consts.GetEntityByIdWithSuccess, nameof(Sale)));
            _saleRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_WhenSaleIsNotFound()
        {
            // Arrange
            var query = new GetSaleByIdQueryBuilder()
                .Build();

            _saleRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Sale)null!);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Data.Should().BeNull();
            result.Status.Should().Be(ResultResponseKind.NotFound);
            result.ErrorMessage.Should().Be(string.Format(Consts.NotFoundEntity, nameof(Sale)));
            result.ErrorDetail.Should().Be(string.Format(Consts.NotFoundEntityById, nameof(Sale), query.Id));
            _saleRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenValidationFails()
        {
            // Arrange
            var query = new GetSaleByIdQueryBuilder()
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