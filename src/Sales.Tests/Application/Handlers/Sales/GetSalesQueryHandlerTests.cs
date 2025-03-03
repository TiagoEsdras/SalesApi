using AutoMapper;
using FluentAssertions;
using Moq;
using Sales.Application.DTOs;
using Sales.Application.Handlers.Sales;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Queries.Sales;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Domain.Entities;
using Sales.Tests.Builders.DTOs;
using Xunit;

namespace Sales.Tests.Application.Handlers.Sales
{
    public class GetSalesQueryHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetSalesQueryHandler _handler;

        public GetSalesQueryHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetSalesQueryHandler(_saleRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSalesListIsEmpty()
        {
            // Arrange
            var query = new GetSalesQuery();

            _saleRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync([]);

            _mapperMock.Setup(x => x.Map<IEnumerable<SaleDto>>(It.IsAny<IEnumerable<Sale>>()))
                .Returns([]);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Data.Should().BeEmpty();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Message.Should().Be(string.Format(Consts.GetEntitiesWithSuccess, nameof(Sale)));
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSalesListHasData()
        {
            // Arrange
            var salesDto = new List<SaleDto> {
                new SaleDtoBuilder().Build()
            };

            var query = new GetSalesQuery();

            _saleRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync([]);

            _mapperMock.Setup(x => x.Map<IEnumerable<SaleDto>>(It.IsAny<IEnumerable<Sale>>()))
                .Returns(salesDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Data.Should().HaveCount(salesDto.Count);
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Message.Should().Be(string.Format(Consts.GetEntitiesWithSuccess, nameof(Sale)));
        }
    }
}