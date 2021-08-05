using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Authorization.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Infrastructure.Persistence.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly DbSet<T> _dbSet;

        protected GenericRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public async Task<IReadOnlyCollection<T>> FindAll()
        {
            return await _dbSet.ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToArrayAsync();
        }

        public Task<T> FindFirst(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefaultAsync(predicate);
        }

        public Task<T> FindLast(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.LastOrDefaultAsync(predicate);
        }

        public Task Insert(T entity)
        {
            _dbSet.Add(entity);
            return Task.CompletedTask;
        }

        public Task BulkInsert(IReadOnlyCollection<T> entities)
        {
            _dbSet.AddRange(entities);
            return Task.CompletedTask;
        }

        public Task Update(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
