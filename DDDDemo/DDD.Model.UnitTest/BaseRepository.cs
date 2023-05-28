using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Model.UnitTest
{
    public class BaseRepository<T, ID> : IRepository<T, ID> where T : class
    {
        protected readonly ShoppingCartDbContext _ctx;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ShoppingCartDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;

            if(filter !=null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<T> Get(ID id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetMany(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? top = null, int? skip = null, params string[] includedFields)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if(includedFields.Length > 0)
            {
                query = includedFields.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
            }

            if(orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }
    }
}
