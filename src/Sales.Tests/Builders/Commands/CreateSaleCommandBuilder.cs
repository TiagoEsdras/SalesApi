using Bogus;
using Sales.Application.Commands.Sales;

namespace Sales.Tests.Builders.Commands
{
    public class CreateSaleCommandBuilder
    {
        private readonly Faker _faker = new();
        private readonly CreateSaleCommand _instance;

        public CreateSaleCommandBuilder()
        {
            _instance = new CreateSaleCommand
            {
                SaleNumber = _faker.Random.AlphaNumeric(10),
                SaleDate = _faker.Date.Recent(),
                CustomerId = Guid.NewGuid(),
                BranchId = Guid.NewGuid(),
                Items =
                [
                    new SaleItemCommand
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = _faker.Random.Int(1, 100),
                        UnitPrice = decimal.Parse(_faker.Commerce.Price())
                    }
                ]
            };
        }

        public CreateSaleCommandBuilder WithSaleNumber(string saleNumber)
        {
            _instance.SaleNumber = saleNumber;
            return this;
        }

        public CreateSaleCommandBuilder WithSaleDate(DateTime saleDate)
        {
            _instance.SaleDate = saleDate;
            return this;
        }

        public CreateSaleCommandBuilder WithCustomerId(Guid customerId)
        {
            _instance.CustomerId = customerId;
            return this;
        }

        public CreateSaleCommandBuilder WithBranchId(Guid branchId)
        {
            _instance.BranchId = branchId;
            return this;
        }

        public CreateSaleCommandBuilder WithItems(IEnumerable<SaleItemCommand> items)
        {
            _instance.Items = items;
            return this;
        }

        public CreateSaleCommand Build() => _instance;
    }
}