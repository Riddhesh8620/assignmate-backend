using AssignmateFunctional.API.DAL.Data.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AssignmateFunctional.DAL.DAL.Services;

public class ServiceDbContextFactory : IDesignTimeDbContextFactory<ServiceDbContext>
{
    public ServiceDbContext CreateDbContext(string[] args)
    {
        string host = Environment.GetEnvironmentVariable("Npgsql__Host")
            ?? throw new KeyNotFoundException("No ENV variable provided for Npgsql__Host.");

        string port = Environment.GetEnvironmentVariable("Npgsql__Port")
            ?? throw new KeyNotFoundException("No ENV variable provided for Npgsql__Port.");

        string database = Environment.GetEnvironmentVariable("Npgsql__Database")
            ?? throw new KeyNotFoundException("No ENV variable provided for Npgsql__Database.");

        string username = Environment.GetEnvironmentVariable("Npgsql__Username")
            ?? throw new KeyNotFoundException("No ENV variable provided for Npgsql__Username.");

        string password = Environment.GetEnvironmentVariable("Npgsql__Password")
            ?? throw new KeyNotFoundException("No ENV variable provided for Npgsql__Password.");

        string connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};Trust Server Certificate=true;Include Error Detail=true";

        DbContextOptionsBuilder<ServiceDbContext> optionsBuilder = new DbContextOptionsBuilder<ServiceDbContext>();
        optionsBuilder.UseNpgsql(connectionString, sqlOptions =>
        {
            _ = sqlOptions.MigrationsAssembly(typeof(ServiceDbContext).Assembly);
            _ = sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "assignmate");
        });

        return new ServiceDbContext(optionsBuilder.Options, null!);
    }
}