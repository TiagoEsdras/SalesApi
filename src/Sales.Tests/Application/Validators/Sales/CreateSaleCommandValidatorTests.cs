using FluentAssertions;
using FluentValidation;
using Moq;
using Sales.Application.Commands.Sales;
using Sales.Application.Validators.Sales;
using Sales.Tests.Builders.Commands;
using Xunit;

namespace Sales.Tests.Application.Validators.Sales
{
    public class CreateSaleCommandValidatorTests
    {
        private readonly CreateSaleCommandValidator _validator;
        private readonly Mock<IValidator<SaleItemCommand>> _saleItemCommandValidatorMock;

        public CreateSaleCommandValidatorTests()
        {
            _saleItemCommandValidatorMock = new Mock<IValidator<SaleItemCommand>>();
            _validator = new CreateSaleCommandValidator(_saleItemCommandValidatorMock.Object);
        }

        [Fact]
        public void Validator_ShouldPass_WhenCreateSaleCommandIsValid()
        {
            // Arrange
            var command = new CreateSaleCommandBuilder()
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validator_ShouldFail_WhenSaleNumberIsEmpty(string invalidSaleNumber)
        {
            // Arrange
            var command = new CreateSaleCommandBuilder()
                .WithSaleNumber(invalidSaleNumber)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateSaleCommand.SaleNumber));
        }

        [Fact]
        public void Validator_ShouldFail_WhenSaleDateIsEmpty()
        {
            // Arrange
            var command = new CreateSaleCommandBuilder()
                .WithSaleDate(default)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateSaleCommand.SaleDate));
        }

        [Fact]
        public void Validator_ShouldFail_WhenCustomerIdIsEmpty()
        {
            // Arrange
            var command = new CreateSaleCommandBuilder()
                .WithCustomerId(default)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateSaleCommand.CustomerId));
        }

        [Fact]
        public void Validator_ShouldFail_WhenBranchIdIsEmpty()
        {
            // Arrange
            var command = new CreateSaleCommandBuilder()
                .WithBranchId(default)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateSaleCommand.BranchId));
        }

        [Fact]
        public void Validator_ShouldFail_WhenItemsAreEmpty()
        {
            // Arrange
            var command = new CreateSaleCommandBuilder()
                .WithItems(null!)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateSaleCommand.Items));
        }

        [Fact]
        public void Validator_ShouldFail_WhenThereAreDuplicatedProductIds()
        {
            // Arrange
            var duplicatedProductId = Guid.NewGuid();
            var items = new List<SaleItemCommand>
            {
                new SaleItemCommandBuilder()
                    .WithProductId(duplicatedProductId)
                    .Build(),
                new SaleItemCommandBuilder()
                    .WithProductId(duplicatedProductId)
                    .Build(),
            };

            var command = new CreateSaleCommandBuilder()
                .WithItems(items)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateSaleCommand.Items));
        }
    }
}