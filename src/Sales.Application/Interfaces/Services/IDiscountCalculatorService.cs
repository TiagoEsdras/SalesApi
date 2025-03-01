namespace Sales.Application.Interfaces.Services
{
    public interface IDiscountCalculatorService
    {
        decimal CalculateDiscount(decimal price, int quantity);
    }
}