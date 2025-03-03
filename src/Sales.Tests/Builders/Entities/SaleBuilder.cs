using Bogus;
using Sales.Domain.Entities;
using System.Reflection;

namespace Sales.Tests.Builders.Entities
{
    namespace Sales.Tests.Builders.Domain
    {
        public class SaleBuilder
        {
            private readonly Faker _faker = new();
            private readonly Sale _instance;

            public SaleBuilder()
            {
                _instance = (Sale)Activator.CreateInstance(typeof(Sale), nonPublic: true)!;

                WithId(Guid.NewGuid())
               .WithSaleNumber(_faker.Random.AlphaNumeric(10))
               .WithDate(_faker.Date.Past())
               .WithCustomerId(Guid.NewGuid())
               .WithBranchId(Guid.NewGuid())
               .WithItems([new SaleItemBuilder().Build()])
               .WithIsCanceled(false);
            }

            public SaleBuilder WithId(Guid id)
            {
                SetPrivateField(nameof(Sale.Id), id);
                return this;
            }

            public SaleBuilder WithSaleNumber(string saleNumber)
            {
                SetPrivateField(nameof(Sale.SaleNumber), saleNumber);
                return this;
            }

            public SaleBuilder WithDate(DateTime date)
            {
                SetPrivateField(nameof(Sale.Date), date);
                return this;
            }

            public SaleBuilder WithCustomerId(Guid customerId)
            {
                SetPrivateField(nameof(Sale.CustomerId), customerId);
                return this;
            }

            public SaleBuilder WithBranchId(Guid branchId)
            {
                SetPrivateField(nameof(Sale.BranchId), branchId);
                return this;
            }

            public SaleBuilder WithItems(IEnumerable<SaleItem> items)
            {
                SetPrivateField(nameof(Sale.Items), items.ToList());
                return this;
            }

            public SaleBuilder WithIsCanceled(bool isCanceled)
            {
                SetPrivateField(nameof(Sale.IsCanceled), isCanceled);
                return this;
            }

            public Sale Build() => _instance;

            private void SetPrivateField(string fieldName, object value)
            {
                var field = _instance.GetType().GetField($"<{fieldName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                field?.SetValue(_instance, value);
            }
        }
    }
}