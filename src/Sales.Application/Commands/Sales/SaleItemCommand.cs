using System.Text.Json.Serialization;

namespace Sales.Application.Commands.Sales
{
    public class SaleItemCommand
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        [JsonIgnore]
        public decimal Discount => CalculateDiscount();

        private decimal CalculateDiscount()
        {
            if (Quantity <= 0 || Quantity > 20)
                throw new InvalidOperationException("Unable to calculate discount for the provided quantity.");

            decimal percentageDiscount = 0;

            if (Quantity < 4)
                return 0;

            if (Quantity < 10)
                percentageDiscount = 10;
            else if (Quantity <= 20)
                percentageDiscount = 20;

            return UnitPrice * Quantity * (percentageDiscount / 100);
        }
    }
}