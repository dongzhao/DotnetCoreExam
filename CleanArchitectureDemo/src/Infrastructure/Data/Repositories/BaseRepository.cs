using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Data.Repositories
{
    public class BaseRepository<T, ID> : IRepository<T, ID> where T : class
    {
        protected readonly AppDbContext _ctx;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(ID id)
        {
            var entity = await Get(id);
            await DeleteAsync(entity);
        }

        public virtual Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual async Task<T> Get(ID id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? top = null, int? skip = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //if(includedFields.Length > 0)
            //{
            //    query = includedFields.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
            //}

            if(orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }
    }
}
