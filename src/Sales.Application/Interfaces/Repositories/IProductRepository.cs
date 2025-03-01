using Sales.Domain.Entities;

namespace Sales.Application.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByIdsAsync(HashSet<Guid> productIds);
    }
}