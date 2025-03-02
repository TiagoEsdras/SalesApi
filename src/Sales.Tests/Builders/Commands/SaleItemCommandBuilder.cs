using Bogus;
using Sales.Application.Commands.Sales;

namespace Sales.Tests.Builders.Commands
{
    public class SaleItemCommandBuilder
    {
        private readonly Faker _faker = new();
        private readonly SaleItemCommand _instance;

        public SaleItemCommandBuilder()
        {
            _instance = new SaleItemCommand
            {
                ProductId = Guid.NewGuid(),
                Quantity = _faker.Random.Int(1, 20),
                UnitPrice = decimal.Parse(_faker.Commerce.Price()),
                TotalPrice = decimal.Parse(_faker.Commerce.Price())
            };
        }

        public SaleItemCommandBuilder WithProductId(Guid productId)
        {
            _instance.ProductId = productId;
            return this;
        }

        public SaleItemCommandBuilder WithQuantity(int quantity)
        {
            _instance.Quantity = quantity;
            return this;
        }

        public SaleItemCommandBuilder WithUnitPrice(decimal unitPrice)
        {
            _instance.UnitPrice = unitPrice;
            return this;
        }

        public SaleItemCommandBuilder WithTotalPrice(decimal totalPrice)
        {
            _instance.TotalPrice = totalPrice;
            return this;
        }

        public SaleItemCommand Build() => _instance;
    }
}