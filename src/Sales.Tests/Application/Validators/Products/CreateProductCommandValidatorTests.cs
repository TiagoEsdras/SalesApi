using FluentAssertions;
using Sales.Application.Commands.Products;
using Sales.Application.Validators.Products;
using Sales.Tests.Builders.Commands;
using Xunit;

namespace Sales.Tests.Application.Validators.Products
{
    public class CreateProductCommandValidatorTests
    {
        private readonly CreateProductCommandValidator _validator;

        public CreateProductCommandValidatorTests()
        {
            _validator = new CreateProductCommandValidator();
        }

        [Fact]
        public void Validator_ShouldPass_WhenCommandIsValid()
        {
            // Arrange
            var command = new CreateProductCommandBuilder()
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
        public void Validator_ShouldFail_WhenTitleIsEmpty(string invalidTitle)
        {
            // Arrange
            var command = new CreateProductCommandBuilder()
                .WithTitle(invalidTitle)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateProductCommand.Title));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validator_ShouldFail_WhenPriceIsZeroOrLess(decimal invalidPrice)
        {
            // Arrange
            var command = new CreateProductCommandBuilder()
                .WithPrice(invalidPrice)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateProductCommand.Price));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validator_ShouldFail_WhenDescriptionIsEmpty(string invalidDescription)
        {
            // Arrange
            var command = new CreateProductCommandBuilder()
                .WithDescription(invalidDescription)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateProductCommand.Description));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validator_ShouldFail_WhenCategoryIsEmpty(string invalidCategory)
        {
            // Arrange
            var command = new CreateProductCommandBuilder()
                .WithCategory(invalidCategory)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateProductCommand.Category));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validator_ShouldFail_WhenImageIsEmpty(string invalidImage)
        {
            // Arrange
            var command = new CreateProductCommandBuilder()
                .WithImage(invalidImage)
                .Build();

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(CreateProductCommand.Image));
        }
    }
}