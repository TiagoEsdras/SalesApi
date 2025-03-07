﻿using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Moq;
using Sales.Application.DTOs;
using Sales.Application.Handlers.Products;
using Sales.Application.Interfaces.Repositories;
using Sales.Application.Queries.Products;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using Sales.Application.Validators.Products;
using Sales.Domain.Entities;
using Sales.Tests.Builders.DTOs;
using Xunit;

namespace Sales.Tests.Application.Handlers.Products
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetProductByIdQueryHandler _handler;
        private readonly GetProductByIdQueryValidator _validator;

        public GetProductByIdQueryHandlerTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _validator = new GetProductByIdQueryValidator();
            _handler = new GetProductByIdQueryHandler(_mockProductRepository.Object, _mockMapper.Object, _validator);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenProductIsFound()
        {
            // Arrange
            var query = new GetProductByIdQuery(Guid.NewGuid());

            var productDto = new ProductDtoBuilder()
                .WithId(query.Id)
                .Build();

            _mockProductRepository.Setup(r => r.GetByIdAsync(query.Id)).ReturnsAsync(new Product());
            _mockMapper.Setup(m => m.Map<ProductDto>(It.IsAny<Product>())).Returns(productDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.Success);
            result.Data.Should().BeEquivalentTo(productDto);
            result.Message.Should().Be(string.Format(Consts.GetEntityByIdWithSuccess, nameof(Product)));
            _mockProductRepository.Verify(r => r.GetByIdAsync(query.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_WhenProductIsNotFound()
        {
            // Arrange
            var query = new GetProductByIdQuery(Guid.NewGuid());

            _mockProductRepository.Setup(r => r.GetByIdAsync(query.Id)).ReturnsAsync((Product)null!);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultResponseKind.NotFound);
            result.ErrorType.Should().Be(ErrorType.DataNotFound);
            result.ErrorMessage.Should().Be(string.Format(Consts.NotFoundEntity, nameof(Product)));
            result.ErrorDetail.Should().Be(string.Format(Consts.NotFoundEntityById, nameof(Product), query.Id));
            _mockProductRepository.Verify(r => r.GetByIdAsync(query.Id), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenValidationFails()
        {
            // Arrange
            var query = new GetProductByIdQuery(Guid.Empty);

            // Act
            Func<Task> act = () => _handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
            _mockProductRepository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}