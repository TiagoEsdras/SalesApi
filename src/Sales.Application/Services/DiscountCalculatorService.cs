using Sales.Application.Interfaces.Services;

namespace Sales.Application.Services
{
    public class DiscountCalculatorService : IDiscountCalculatorService
    {
        public decimal CalculateDiscount(decimal price, int quantity)
        {
            if (quantity <= 0 || quantity > 20)
                throw new InvalidOperationException("Unable to calculate discount for the provided quantity.");

            decimal percentageDiscount = 0;

            if (quantity < 4)
                return percentageDiscount;

            if (quantity < 10)
                percentageDiscount = 10;
            else if (quantity <= 20)
                percentageDiscount = 20;

            return price * (percentageDiscount / 100);
        }
    }
}