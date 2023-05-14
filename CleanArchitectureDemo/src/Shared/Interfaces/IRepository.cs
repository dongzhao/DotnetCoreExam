
using System.Linq.Expressions;

namespace Shared.Interfaces
{
    public interface IRepository<T, ID> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(ID id);
        Task<T> Get(ID id);
        Task<IEnumerable<T>> GetMany(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? pageSize = null,
            int? pageNumber = null);
    }
}
