using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Domain.Entities
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("title")]
        [Required]
        public string Title { get; private set; }

        [Column("price")]
        [Required]
        public decimal Price { get; private set; }

        [Column("description")]
        [Required]
        public string Description { get; private set; }

        [Column("category")]
        [Required]
        public string Category { get; private set; }

        [Column("image_url")]
        [Required]
        public string Image { get; private set; }
    }
}