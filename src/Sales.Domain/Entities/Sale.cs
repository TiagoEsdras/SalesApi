namespace Sales.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; }
        public string SaleNumber { get; private set; }
        public DateTime Date { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid BranchId { get; private set; }
        public decimal TotalAmount { get => GetTotalAmount(); }
        public bool IsCanceled { get; private set; }
        public IEnumerable<SaleItem> Items { get; private set; }

        private decimal GetTotalAmount() => Items.Sum(i => i.Total - i.Discount);
    }
}