using AssignmateFunctional.Common.Common;

namespace AssignmateFunctional.API.Business;

public interface IUserManagementService
{
	Task<Guid> RegisterAsync(RegisterUserDto registerUserDto);
}