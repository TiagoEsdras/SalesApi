namespace Sales.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get => CalculateDiscount(); }
        public decimal Total { get; private set; }
        public Guid SaleId { get; private set; }
        public bool IsCanceled { get; private set; }

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