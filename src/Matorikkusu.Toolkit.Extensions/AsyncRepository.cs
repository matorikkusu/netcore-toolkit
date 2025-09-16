using System.ComponentModel;
using System.Linq.Expressions;
using Matorikkusu.Toolkit.Extensions.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Matorikkusu.Toolkit.Extensions
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        protected readonly DbSet<TEntity> DbSet;

        public AsyncRepository(DbContext dbContext)
        {
            DbSet = dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            foreach (var includeProperty in includeProperties
                         .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await (orderBy != null ? orderBy(query).ToListAsync() : query.ToListAsync());
        }

        public async Task<PaginationResult<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> expression = null,
            int page = 1,
            int pageSize = 10,
            params SortExpression<TEntity>[] sortExpressions)
        {
            IQueryable<TEntity> query = DbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            query = GetOrderedQueryable(query, sortExpressions);

            return await query.GetPagination(page, pageSize);
        }

        public async Task<PaginationResult<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> expression = null, int page = 0, int pageSize = 10,
            params SortExpression<TEntity>[] sortExpressions)
        {
            IQueryable<TEntity> query = DbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            query = GetOrderedQueryable(query, sortExpressions);

            return await query.GetPagination(selector, page, pageSize);
        }

        public IQueryable<TEntity> GetQueryAble(int page = 0, int pageSize = 10,
            params SortExpression<TEntity>[] sortExpressions)
        {
            IQueryable<TEntity> query = DbSet;

            query = GetOrderedQueryable(query, sortExpressions);

            return query;
        }

        public IQueryable<TEntity> GetQueryAble()
        {
            IQueryable<TEntity> query = DbSet;
            return query;
        }

        public IQueryable<TEntity> GetQueryAble(Expression<Func<TEntity, bool>> expression)
        {
            var query = DbSet.Where(expression);
            return query;
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.FirstOrDefaultAsync(expression);
        }

        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.SingleOrDefaultAsync(expression);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public IQueryable<TEntity> GetOrderedQueryable(IQueryable<TEntity> query,
            params SortExpression<TEntity>[] sortExpressions)
        {
            IOrderedQueryable<TEntity> orderedQuery = null;
            for (var i = 0; i < sortExpressions.Length; i++)
            {
                if (i == 0 || orderedQuery == null)
                {
                    orderedQuery = sortExpressions[i].SortDirection == ListSortDirection.Ascending
                        ? query.OrderBy(sortExpressions[i].SortBy)
                        : query.OrderByDescending(sortExpressions[i].SortBy);
                    continue;
                }

                orderedQuery = sortExpressions[i].SortDirection == ListSortDirection.Ascending
                    ? orderedQuery.ThenBy(sortExpressions[i].SortBy)
                    : orderedQuery.ThenByDescending(sortExpressions[i].SortBy);
            }

            query = orderedQuery ?? query;

            return query;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return DbSet.Where(expression);
        }

        public async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
}