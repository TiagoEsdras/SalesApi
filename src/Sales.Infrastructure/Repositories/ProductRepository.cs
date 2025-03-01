using Microsoft.EntityFrameworkCore;
using Sales.Application.Interfaces.Repositories;
using Sales.Domain.Entities;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(SaleDbContext context) : base(context)
        {
        }
    }
}