﻿namespace Sales.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task<T?> UpdateAsync(T entity);

        Task<bool> DeleteAsync(Guid id);
    }
}