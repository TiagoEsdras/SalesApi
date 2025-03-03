using Bogus;
using Sales.Application.DTOs;

namespace Sales.Tests.Builders.DTOs
{
    public class SaleItemDtoBuilder
    {
        private readonly Faker _faker = new();
        private readonly SaleItemDto _instance;

        public SaleItemDtoBuilder()
        {
            _instance = new SaleItemDto
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = _faker.Random.Int(1, 100),
                UnitPrice = _faker.Finance.Amount(),
                Discount = _faker.Finance.Amount(0, 50),
                Total = _faker.Finance.Amount(),
                SaleId = Guid.NewGuid(),
                IsCanceled = _faker.Random.Bool()
            };
        }

        public SaleItemDtoBuilder WithId(Guid id)
        {
            _instance.Id = id;
            return this;
        }

        public SaleItemDtoBuilder WithProductId(Guid productId)
        {
            _instance.ProductId = productId;
            return this;
        }

        public SaleItemDtoBuilder WithQuantity(int quantity)
        {
            _instance.Quantity = quantity;
            return this;
        }

        public SaleItemDtoBuilder WithUnitPrice(decimal unitPrice)
        {
            _instance.UnitPrice = unitPrice;
            return this;
        }

        public SaleItemDtoBuilder WithDiscount(decimal discount)
        {
            _instance.Discount = discount;
            return this;
        }

        public SaleItemDtoBuilder WithTotal(decimal total)
        {
            _instance.Total = total;
            return this;
        }

        public SaleItemDtoBuilder WithSaleId(Guid saleId)
        {
            _instance.SaleId = saleId;
            return this;
        }

        public SaleItemDtoBuilder WithIsCanceled(bool isCanceled)
        {
            _instance.IsCanceled = isCanceled;
            return this;
        }

        public SaleItemDto Build() => _instance;
    }
}