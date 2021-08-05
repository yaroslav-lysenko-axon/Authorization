using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Authorization.Domain.Repositories
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<IReadOnlyCollection<T>> FindAll();
        Task<IReadOnlyCollection<T>> Find(Expression<Func<T, bool>> predicate);
        Task<T> FindFirst(Expression<Func<T, bool>> predicate);
        Task<T> FindLast(Expression<Func<T, bool>> predicate);
        Task Insert(T entity);
        Task BulkInsert(IReadOnlyCollection<T> entities);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
