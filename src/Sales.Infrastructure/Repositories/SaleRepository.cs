using Sales.Application.Interfaces.Repositories;
using Sales.Domain.Entities;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(SaleDbContext context) : base(context)
        {
        }
    }
}