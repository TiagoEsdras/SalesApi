using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Domain.Entities
{
    [Table("sale_items")]
    public class SaleItem
    {
        [Column("id")]
        [Key]
        public Guid Id { get; private set; }

        [Column("product_id")]
        [Required]
        public Guid ProductId { get; private set; }

        [Column("quantity")]
        [Required]
        public int Quantity { get; private set; }

        [Column("unit_price")]
        [Required]
        public decimal UnitPrice { get; private set; }

        [Column("discount")]
        [Required]
        public decimal Discount { get; private set; }

        [Column("total")]
        [Required]
        public decimal Total { get; private set; }

        [Column("sale_id")]
        [Required]
        public Guid SaleId { get; private set; }

        [ForeignKey("is_canceled")]
        [Required]
        public bool IsCanceled { get; private set; }

        public void Cancel()
        {
            if (IsCanceled)
                throw new InvalidOperationException("Sale item is already canceled.");
            IsCanceled = true;
        }
    }
}