using AssignmateFunctional.API.DAL.Services;
using AssignmateFunctional.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssignmateFunctional.API.DAL.Data.EfCore;

public class ServiceDbContext(
    DbContextOptions<ServiceDbContext> options,
    IAuditScope auditScope)
    : BaseDbContext(options, auditScope)
{
    public DbSet<AssignmateUser> AssignmateUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        _ = optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _ = modelBuilder.HasDefaultSchema("assignmate");

        _ = modelBuilder.Entity<AssignmateUser>()
            .Property(p => p.Role)
            .HasConversion<int>();

        _ = modelBuilder.Entity<AssignmateUser>()
            .HasIndex(p => new { p.Role, p.Email })
            .IsUnique();
    }
}