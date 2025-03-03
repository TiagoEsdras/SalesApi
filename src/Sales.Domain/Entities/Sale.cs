using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Domain.Entities
{
    [Table("sales")]
    public class Sale
    {
        [Key]
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("sale_number")]
        [Required]
        public string SaleNumber { get; private set; }

        [Column("date")]
        [Required]
        public DateTime Date { get; private set; }

        [Column("customer_id")]
        [Required]
        public Guid CustomerId { get; private set; }

        [Column("branch_id")]
        [Required]
        public Guid BranchId { get; private set; }

        [Column("total_amount")]
        [Required]
        public decimal TotalAmount { get; private set; }

        [Column("is_canceled")]
        [Required]
        public bool IsCanceled { get; private set; }

        public ICollection<SaleItem> Items { get; private set; }

        public void Cancel()
        {
            if (IsCanceled)
                throw new InvalidOperationException("Sale is already canceled.");
            IsCanceled = true;

            foreach (var item in Items)
                item.Cancel();
        }
    }
}