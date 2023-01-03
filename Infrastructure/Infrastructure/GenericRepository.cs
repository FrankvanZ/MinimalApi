using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Infrastructure;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    #region generic

    protected DbContext _context = null;
    internal DbSet<TEntity?> _dbSet = null;
    internal IUnitOfWork<DbContext> _unitOfWork = null;

    public GenericRepository(DbContext context)
    {
        InitFromContext(context);
    }

    public GenericRepository(IUnitOfWork<DbContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
        InitFromContext(unitOfWork.DbContext);
    }

    private void InitFromContext(DbContext context)
    {
        this._context = context;
        this._dbSet = _context.Set<TEntity>();
    }

    protected virtual IQueryable<TEntity?> BaseQuery()
    {
        return _dbSet;
    }

    public virtual async Task<TEntity?> Single(Expression<Func<TEntity?, bool>> filter = null, params Expression<Func<TEntity?, object>>[] includes)
    {
        var query = await this.Query(filter, includes);
        return query.SingleOrDefault();
    }

    /// <summary>
    /// Opens up a generic query functionality
    /// Don't overuse this method to map business functionality to a query
    /// Create a custom method on the repository instead
    /// </summary>
    /// <param name="filter">optional filter for the query</param>
    /// <param name="includes">optional Includes for the query</param>
    /// <returns>The filtered queryable result</returns>
    public virtual async Task<IQueryable<TEntity?>> Query(Expression<Func<TEntity?, bool>> filter = null, params Expression<Func<TEntity?, object>>[] includes)
    {
        return await Task.Run(() =>
        {
            IQueryable<TEntity?> query = BaseQuery();
            if (filter != null)
            {
                query = query.Where(filter);
                if (includes != null)
                {
                    query = includes.Aggregate(query,
                        (current, include) => current.Include(include));
                }
            }

            return query;
        });
    }

    #endregion

    public async Task<IEnumerable<TEntity?>> GetAll()
    {
        return await BaseQuery().ToListAsync();
    }

    public async Task<IEnumerable<TEntity?>> Find(Expression<Func<TEntity?, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<bool> Add(TEntity? entity)
    {
        await _dbSet.AddAsync(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> AddRange(IEnumerable<TEntity?> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(TEntity entity)
    {
        var returnValue = _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Remove(TEntity? entity)
    {
        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveRange(IEnumerable<TEntity?> entities)
    {
        _dbSet.RemoveRange(entities);
        return await _context.SaveChangesAsync() > 0;
    }
}