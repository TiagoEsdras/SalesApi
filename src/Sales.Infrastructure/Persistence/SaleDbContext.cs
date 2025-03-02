using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;

namespace Sales.Infrastructure.Persistence
{
    public class SaleDbContext : DbContext
    {
        public SaleDbContext(DbContextOptions<SaleDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Primary Keys

            modelBuilder.Entity<Product>()
               .HasKey(s => s.Id);

            modelBuilder.Entity<Sale>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<SaleItem>()
                .HasKey(si => si.Id);

            #endregion Primary Keys

            #region Relationships

            modelBuilder.Entity<Sale>()
                .HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SaleItem>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(si => si.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion Relationships

            #region Includes

            modelBuilder.Entity<Sale>()
                .Navigation(s => s.Items)
                .AutoInclude();

            #endregion Includes

            base.OnModelCreating(modelBuilder);
        }
    }
}