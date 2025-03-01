using Bogus;
using Sales.Application.Commands.Products;

namespace Sales.Tests.Builders.Commands
{
    public class CreateProductCommandBuilder
    {
        private readonly Faker _faker = new();
        private readonly CreateProductCommand _instance;

        public CreateProductCommandBuilder()
        {
            _instance = new CreateProductCommand
            {
                Title = _faker.Commerce.ProductName(),
                Price = decimal.Parse(_faker.Commerce.Price()),
                Description = _faker.Commerce.ProductDescription(),
                Category = _faker.Commerce.Department(),
                Image = _faker.Image.PicsumUrl()
            };
        }

        public CreateProductCommandBuilder WithTitle(string title)
        {
            _instance.Title = title;
            return this;
        }

        public CreateProductCommandBuilder WithPrice(decimal price)
        {
            _instance.Price = price;
            return this;
        }

        public CreateProductCommandBuilder WithDescription(string description)
        {
            _instance.Description = description;
            return this;
        }

        public CreateProductCommandBuilder WithCategory(string category)
        {
            _instance.Category = category;
            return this;
        }

        public CreateProductCommandBuilder WithImage(string image)
        {
            _instance.Image = image;
            return this;
        }

        public CreateProductCommand Build() => _instance;
    }
}