using Bogus;
using Sales.Application.DTOs;

namespace Sales.Tests.Builders.DTOs
{
    public class SaleDtoBuilder
    {
        private readonly Faker _faker = new();
        private readonly SaleDto _instance;

        public SaleDtoBuilder()
        {
            _instance = new SaleDto
            {
                Id = Guid.NewGuid(),
                SaleNumber = _faker.Commerce.Ean13(),
                Date = _faker.Date.Past(),
                CustomerId = Guid.NewGuid(),
                BranchId = Guid.NewGuid(),
                TotalAmount = decimal.Parse(_faker.Commerce.Price()),
                IsCanceled = _faker.Random.Bool(),
                Items = [new SaleItemDtoBuilder().Build()
                ]
            };
        }

        public SaleDtoBuilder WithId(Guid id)
        {
            _instance.Id = id;
            return this;
        }

        public SaleDtoBuilder WithSaleNumber(string saleNumber)
        {
            _instance.SaleNumber = saleNumber;
            return this;
        }

        public SaleDtoBuilder WithDate(DateTime date)
        {
            _instance.Date = date;
            return this;
        }

        public SaleDtoBuilder WithCustomerId(Guid customerId)
        {
            _instance.CustomerId = customerId;
            return this;
        }

        public SaleDtoBuilder WithBranchId(Guid branchId)
        {
            _instance.BranchId = branchId;
            return this;
        }

        public SaleDtoBuilder WithTotalAmount(decimal totalAmount)
        {
            _instance.TotalAmount = totalAmount;
            return this;
        }

        public SaleDtoBuilder WithIsCanceled(bool isCanceled)
        {
            _instance.IsCanceled = isCanceled;
            return this;
        }

        public SaleDtoBuilder WithItems(IEnumerable<SaleItemDto> items)
        {
            _instance.Items = items;
            return this;
        }

        public SaleDto Build() => _instance;
    }
}