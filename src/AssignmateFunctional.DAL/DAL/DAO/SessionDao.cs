using AssignmateFunctional.API.DAL.Data.EfCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AssignmateFunctional.API.DAL.DAO;

public class SessionDao(ServiceDbContext dbContext) : ISessionDao
{
    private IDbContextTransaction? _transaction;
    private readonly ServiceDbContext _dbContext = dbContext;

    public async Task<T> AddAsync<T>(T entity) where T : class
    {
        _ = await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
    {
        await _dbContext.Set<T>().AddRangeAsync(entities);
    }

    public Task UpdateAsync<T>(T entity) where T : class
    {
        _ = _dbContext.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync<T>(T entity) where T : class
    {
        _ = _dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
        {
            return;
        }

        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> CommitAsync()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No active transaction");
        }

        int entitiesAffected = await _dbContext.SaveChangesAsync();
        await _transaction.CommitAsync();
        await DisposeTransactionAsync();
        return entitiesAffected;
    }

    public async Task RollbackAsync()
    {
        if (_transaction == null)
        {
            return;
        }

        await _transaction.RollbackAsync();
        await DisposeTransactionAsync();
    }

    private async Task DisposeTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}