using AssignmateFunctional.API.Entities;
using System.Linq.Expressions;

namespace AssignmateFunctional.API.DAL.DAO;

public interface IBaseDao<T>
    where T : BaseEntity
{
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate, bool tracking = false);
    Task<T?> GetByIdAsync(params object[] keys);
    IQueryable<T> Query(bool tracking = false);
    Task<int> SaveChangesAsync();
    Task UpdateAsync(T entity);
}