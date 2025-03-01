using FluentAssertions;
using Sales.Application.Services;
using Xunit;

namespace Sales.Tests.Application.Services
{
    public class DiscountCalculatorServiceTests
    {
        private readonly DiscountCalculatorService _discountCalculatorService;

        public DiscountCalculatorServiceTests()
        {
            _discountCalculatorService = new DiscountCalculatorService();
        }

        [Theory]
        [InlineData(1, 100, 0)]
        [InlineData(3, 100, 0)]
        [InlineData(4, 100, 10)]
        [InlineData(9, 100, 10)]
        [InlineData(10, 100, 20)]
        [InlineData(20, 100, 20)]
        public void CalculatePercentageDiscount_ShouldReturnExpectedDiscount(int quantity, decimal price, decimal expectedDiscount)
        {
            // Act
            var discount = _discountCalculatorService.CalculateDiscount(price, quantity);

            // Assert
            discount.Should().Be(expectedDiscount);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(21)]
        public void CalculatePercentageDiscount_ShouldThrowInvalidOperationException_WhenQuantityIsInvalid(int invalidQuantity)
        {
            // Act
            Action action = () => _discountCalculatorService.CalculateDiscount(100, invalidQuantity);

            // Assert
            action.Should().Throw<InvalidOperationException>().WithMessage("Unable to calculate discount for the provided quantity.");
        }
    }
}