using Bogus;
using Sales.Application.DTOs;

namespace Sales.Tests.Builders.DTOs
{
    public class ProductDtoBuilder
    {
        private readonly Faker _faker = new();
        private readonly ProductDto _instance;

        public ProductDtoBuilder()
        {
            _instance = new ProductDto
            {
                Id = Guid.NewGuid(),
                Title = _faker.Commerce.ProductName(),
                Price = decimal.Parse(_faker.Commerce.Price()),
                Description = _faker.Commerce.ProductDescription(),
                Category = _faker.Commerce.Department(),
                Image = _faker.Image.PicsumUrl()
            };
        }

        public ProductDtoBuilder WithId(Guid id)
        {
            _instance.Id = id;
            return this;
        }

        public ProductDtoBuilder WithTitle(string title)
        {
            _instance.Title = title;
            return this;
        }

        public ProductDtoBuilder WithPrice(decimal price)
        {
            _instance.Price = price;
            return this;
        }

        public ProductDtoBuilder WithDescription(string description)
        {
            _instance.Description = description;
            return this;
        }

        public ProductDtoBuilder WithCategory(string category)
        {
            _instance.Category = category;
            return this;
        }

        public ProductDtoBuilder WithImage(string image)
        {
            _instance.Image = image;
            return this;
        }

        public ProductDto Build() => _instance;
    }
}