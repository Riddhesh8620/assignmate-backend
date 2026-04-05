using AssignmateFunctional.API.Entities;

namespace AssignmateFunctional.API.DAL.DAO;

public interface IServiceDao<T>
	: IBaseDao<T>
	where T : BaseEntity
{
}
