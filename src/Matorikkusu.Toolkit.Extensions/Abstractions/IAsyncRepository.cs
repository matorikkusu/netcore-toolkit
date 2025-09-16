using System.Linq.Expressions;

namespace Matorikkusu.Toolkit.Extensions.Abstractions
{
    public interface IAsyncRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<PaginationResult<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> expression = null,
            int page = 0,
            int pageSize = 10,
            params SortExpression<TEntity>[] sortExpressions);
        
        Task<PaginationResult<TResult>> GetAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> expression = null,
            int page = 0,
            int pageSize = 10,
            params SortExpression<TEntity>[] sortExpressions);

        IQueryable<TEntity> GetQueryAble(
            int page = 0,
            int pageSize = 10,
            params SortExpression<TEntity>[] sortExpressions);

        IQueryable<TEntity> GetQueryAble();
        IQueryable<TEntity> GetQueryAble(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        Task<TEntity> GetByIdAsync(object id);

        IQueryable<TEntity> GetOrderedQueryable(IQueryable<TEntity> query,
            params SortExpression<TEntity>[] sortExpressions);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);

        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);
    }
}