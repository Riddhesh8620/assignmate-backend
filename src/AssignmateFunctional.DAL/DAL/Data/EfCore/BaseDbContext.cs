using AssignmateFunctional.API.DAL.Services;
using AssignmateFunctional.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AssignmateFunctional.API.DAL.Data.EfCore;

public abstract class BaseDbContext
    : DbContext
{
    private readonly IAuditScope _auditScope;

    protected BaseDbContext(DbContextOptions options, IAuditScope auditScope)
        : base(options)
    {
        _auditScope = auditScope;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var currentUserId = _auditScope.GetUserId();

        var currentTime = DateTime.UtcNow;

        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (EntityEntry<BaseEntity> entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity.Id == Guid.Empty)
                {
                    entry.Entity.Id = Guid.CreateVersion7();
                }

                entry.Entity.AddedOn = currentTime;
                entry.Entity.AddedBy = currentUserId;

                entry.Entity.UpdatedOn = currentTime;
                entry.Entity.UpdatedBy = currentUserId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedOn = currentTime;
                entry.Entity.UpdatedBy = currentUserId;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}