using Bogus;
using Sales.Domain.Entities;
using System.Reflection;

namespace Sales.Tests.Builders.Entities
{
    namespace Sales.Tests.Builders.Domain
    {
        public class SaleItemBuilder
        {
            private readonly Faker _faker = new();
            private readonly SaleItem _instance;

            public SaleItemBuilder()
            {
                _instance = (SaleItem)Activator.CreateInstance(typeof(SaleItem), nonPublic: true)!;

                WithId(Guid.NewGuid())
                .WithProductId(Guid.NewGuid())
                .WithQuantity(_faker.Random.Int(1, 20))
                .WithUnitPrice(decimal.Parse(_faker.Commerce.Price()))
                .WithTotal(decimal.Parse(_faker.Commerce.Price()))
                .WithSaleId(Guid.NewGuid())
                .WithIsCanceled(false);
            }

            public SaleItemBuilder WithId(Guid id)
            {
                SetPrivateField(nameof(SaleItem.Id), id);
                return this;
            }

            public SaleItemBuilder WithProductId(Guid productId)
            {
                SetPrivateField(nameof(SaleItem.ProductId), productId);
                return this;
            }

            public SaleItemBuilder WithQuantity(int quantity)
            {
                SetPrivateField(nameof(SaleItem.Quantity), quantity);
                return this;
            }

            public SaleItemBuilder WithUnitPrice(decimal unitPrice)
            {
                SetPrivateField(nameof(SaleItem.UnitPrice), unitPrice);
                return this;
            }

            public SaleItemBuilder WithTotal(decimal total)
            {
                SetPrivateField(nameof(SaleItem.Total), total);
                return this;
            }

            public SaleItemBuilder WithSaleId(Guid saleId)
            {
                SetPrivateField(nameof(SaleItem.SaleId), saleId);
                return this;
            }

            public SaleItemBuilder WithIsCanceled(bool isCanceled)
            {
                SetPrivateField(nameof(SaleItem.IsCanceled), isCanceled);
                return this;
            }

            public SaleItem Build() => _instance;

            private void SetPrivateField(string fieldName, object value)
            {
                var field = _instance.GetType().GetField($"<{fieldName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                field?.SetValue(_instance, value);
            }
        }
    }
}