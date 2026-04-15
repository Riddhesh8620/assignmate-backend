using AssignmateFunctional.API.DAL.Services;
using AssignmateFunctional.API.Entities;
using AssignmateFunctional.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssignmateFunctional.API.DAL.Data.EfCore;

public class ServiceDbContext(DbContextOptions<ServiceDbContext> options)
    : BaseDbContext(options)
{
    public DbSet<AssignmateUser> AssignmateUsers { get; set; }
    public DbSet<Assignment> Assignments { get; set; }

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

        _ = modelBuilder.Entity<Assignment>()
            .Property(p => p.Status)
            .HasConversion<string>();

        _ = modelBuilder.Entity<Assignment>()
            .HasIndex(p => p.Status);
       
        _ = modelBuilder.Entity<Assignment>()
            .HasIndex(p => p.Subject);
    }
}