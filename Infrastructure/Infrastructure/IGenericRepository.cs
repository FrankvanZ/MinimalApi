using System.Linq.Expressions;

namespace Infrastructure.Infrastructure;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> Single(Expression<Func<TEntity?, bool>> filter = null, params Expression<Func<TEntity?, object>>[] includes);
    Task<IQueryable<TEntity?>> Query(Expression<Func<TEntity?, bool>> filter = null, params Expression<Func<TEntity?, object>>[] includes);
    Task<IEnumerable<TEntity?>> GetAll();
    Task<IEnumerable<TEntity?>> Find(Expression<Func<TEntity?, bool>> predicate);
    Task<bool> Add(TEntity? entity);
    Task<bool> AddRange(IEnumerable<TEntity?> entities);
    Task<bool> Update(TEntity entity);
    Task<bool> Remove(TEntity? entity);
    Task<bool> RemoveRange(IEnumerable<TEntity?> entities);
}