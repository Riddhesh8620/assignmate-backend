using AssignmateFunctional.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AssignmateFunctional.API.DAL.DAO;

public class BaseDao<T> : IBaseDao<T>
    where T : BaseEntity
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public BaseDao(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    // ---------------- CREATE ----------------

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    // ---------------- UPDATE ----------------

    public Task UpdateAsync(T entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    // ---------------- DELETE ----------------

    public Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    // ---------------- READ ----------------

    public async Task<T?> GetByIdAsync(params object[] keys)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<object>(e, "Id").Equals(keys[0]));
    }

    public IQueryable<T> Query(bool tracking = false)
    {
        return tracking ? _dbSet : _dbSet.AsNoTracking();
    }

    public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, bool tracking = false)
    {
        return tracking
            ? await _dbSet.Where(predicate).ToListAsync()
            : await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.AnyAsync(predicate);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}