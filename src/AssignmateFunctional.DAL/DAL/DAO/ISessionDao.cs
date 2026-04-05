namespace AssignmateFunctional.API.DAL.DAO;

public interface ISessionDao
{
    Task<T> AddAsync<T>(T entity) where T : class;
    Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class;
    Task BeginTransactionAsync();
    Task<int> CommitAsync();
    Task DeleteAsync<T>(T entity) where T : class;
    Task RollbackAsync();
    Task<int> SaveChangesAsync();
    Task UpdateAsync<T>(T entity) where T : class;
}