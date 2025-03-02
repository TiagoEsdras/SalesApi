using FluentAssertions;
using Sales.Application.Commands.Sales;
using Sales.Application.Validators.Sales;
using Sales.Tests.Builders.Commands;
using Xunit;

namespace Sales.Tests.Application.Validators.Sales
{
    public class SaleItemCommandValidatorTests
    {
        private readonly SaleItemCommandValidator _validator;

        public SaleItemCommandValidatorTests()
        {
            _validator = new SaleItemCommandValidator();
        }

        [Fact]
        public void Validator_ShouldPass_WhenSaleItemCommandIsValid()
        {
            // Arrange
            var command = new SaleItemCommandBuilder()
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validator_ShouldFail_WhenProductIdIsEmpty()
        {
            // Arrange
            var command = new SaleItemCommandBuilder()
                .WithProductId(default)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(SaleItemCommand.ProductId));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(21)]
        public void Validator_ShouldFail_WhenQuantityIsInvalid(int invalidQuantity)
        {
            // Arrange
            var command = new SaleItemCommandBuilder()
                .WithQuantity(invalidQuantity)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(SaleItemCommand.Quantity));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-0.1)]
        public void Validator_ShouldFail_WhenUnitPriceIsInvalid(decimal invalidUnitPrice)
        {
            // Arrange
            var command = new SaleItemCommandBuilder()
                .WithUnitPrice(invalidUnitPrice)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(SaleItemCommand.UnitPrice));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validator_ShouldFail_WhenTotalPriceIsInvalid(decimal invalidTotalPrice)
        {
            // Arrange
            var command = new SaleItemCommandBuilder()
                .WithTotalPrice(invalidTotalPrice)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(SaleItemCommand.TotalPrice));
        }
    }
}