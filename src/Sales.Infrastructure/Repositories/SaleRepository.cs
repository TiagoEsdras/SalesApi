using Microsoft.EntityFrameworkCore;
using Sales.Application.Interfaces.Repositories;
using Sales.Domain.Entities;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        private readonly SaleDbContext _context;
        public SaleRepository(SaleDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Sale?> GetWithItemsAsync(Guid saleId)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == saleId);
        }
    }
}