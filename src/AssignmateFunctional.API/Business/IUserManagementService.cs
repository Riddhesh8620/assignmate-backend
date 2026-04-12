using AssignmateFunctional.Common.DTO;

namespace AssignmateFunctional.API.Business;

public interface IUserManagementService
{
	Task<UserStoreDto> HandleLoginAsync(LoginDto loginDto);
	Task<UserStoreDto> RegisterAsync(RegisterUserDto registerUserDto);
}