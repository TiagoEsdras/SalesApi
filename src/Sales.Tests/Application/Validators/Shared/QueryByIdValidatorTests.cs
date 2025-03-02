using FluentAssertions;
using Sales.Application.Queries;
using Sales.Application.Validators.Shared;
using Xunit;

namespace Sales.Tests.Application.Validators.Shared
{
    public class QueryByIdValidatorTests
    {
        private readonly QueryByIdValidator<TestQueryById> _validator;

        public QueryByIdValidatorTests()
        {
            _validator = new();
        }

        [Fact]
        public void Validator_ShouldSucceed_WhenIdIsValid()
        {
            // Arrange
            var query = new TestQueryById { Id = Guid.NewGuid() };

            // Act
            var result = _validator.Validate(query);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validator_ShouldFail_WhenIdIsEmpty()
        {
            // Arrange
            var query = new TestQueryById { Id = Guid.Empty };

            // Act
            var result = _validator.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(IQueryById.Id));
        }

        private class TestQueryById : IQueryById
        {
            public Guid Id { get; set; }
        }
    }
}