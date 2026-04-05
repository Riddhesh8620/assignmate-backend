using AssignmateFunctional.API.DAL.Data.EfCore;
using AssignmateFunctional.API.Entities;

namespace AssignmateFunctional.API.DAL.DAO;

public class ServiceDao<T>(ServiceDbContext dbContext)
	: BaseDao<T>(dbContext)
	, IServiceDao<T>
	where T : BaseEntity
{
}
