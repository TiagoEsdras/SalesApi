using Bogus;
using Sales.Domain.Entities;
using System.Reflection;

namespace Sales.Tests.Builders.Entities
{
    public class ProductBuilder
    {
        private readonly Faker _faker = new();
        private readonly Product _instance;

        public ProductBuilder()
        {
            _instance = (Product)Activator.CreateInstance(typeof(Product), nonPublic: true)!;

            WithId(Guid.NewGuid())
            .WithTitle(_faker.Commerce.ProductName())
            .WithDescription(_faker.Commerce.ProductDescription())
            .WithPrice(_faker.Finance.Amount())
            .WithCategory(_faker.Commerce.Categories(1).First())
            .WithImage(_faker.Image.PicsumUrl());
        }

        public ProductBuilder WithId(Guid id)
        {
            SetPrivateField(nameof(Product.Id), id);
            return this;
        }

        public ProductBuilder WithTitle(string title)
        {
            SetPrivateField(nameof(Product.Title), title);
            return this;
        }

        public ProductBuilder WithDescription(string description)
        {
            SetPrivateField(nameof(Product.Description), description);
            return this;
        }

        public ProductBuilder WithPrice(decimal price)
        {
            SetPrivateField(nameof(Product.Price), price);
            return this;
        }

        public ProductBuilder WithCategory(string category)
        {
            SetPrivateField(nameof(Product.Category), category);
            return this;
        }

        public ProductBuilder WithImage(string image)
        {
            SetPrivateField(nameof(Product.Image), image);
            return this;
        }

        public Product Build() => _instance;

        private void SetPrivateField(string fieldName, object value)
        {
            var field = _instance.GetType().GetField($"<{fieldName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            field?.SetValue(_instance, value);
        }
    }
}