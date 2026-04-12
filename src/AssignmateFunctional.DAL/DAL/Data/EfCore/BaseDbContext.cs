using Microsoft.EntityFrameworkCore;

namespace AssignmateFunctional.API.DAL.Data.EfCore;

public abstract class BaseDbContext(DbContextOptions options)
        : DbContext(options)
{
}