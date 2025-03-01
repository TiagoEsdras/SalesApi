using Microsoft.EntityFrameworkCore;
using Sales.Application.Interfaces.Repositories;
using Sales.Domain.Entities;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly SaleDbContext _context;

        public ProductRepository(SaleDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetByIdsAsync(HashSet<Guid> productIds)
        {
            return await _context.Products
                          .Where(p => productIds.Contains(p.Id))
                          .ToListAsync();
        }
    }
}