using AssignmateFunctional.API.DAL.DAO;
using AssignmateFunctional.API.DAL.Data.EfCore;
using AssignmateFunctional.API.DAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssignmateFunctional.API.DAL.Dependency;

public static class DependencyInjection
{

    public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddScoped<IAuditScope, AuditScope>();


        string connectionString = configuration.GetValue<string>("NpgsqlConnectionString")
            ?? throw new KeyNotFoundException("No ENV variable provided for DbConnection string.");

        _ = services.AddDbContext<ServiceDbContext>(options =>
        {
            _ = options.UseNpgsql(connectionString, sqlOptions =>
            {
                _ = sqlOptions.MigrationsAssembly(typeof(ServiceDbContext).Assembly);
                _ = sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "assignmate");
            });
        });

        _ = services.RegisterDALServices();

        return services;
    }

    private static IServiceCollection RegisterDALServices(this IServiceCollection services)
    {
        _ = services.AddScoped<ISessionDao, SessionDao>();
        _ = services.AddScoped(typeof(IServiceDao<>), typeof(ServiceDao<>));

        return services;
    }
}