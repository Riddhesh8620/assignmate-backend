using AssignmateFunctional.API.Common;
using AssignmateFunctional.API.DAL.DAO;
using AssignmateFunctional.API.Entities;
using AssignmateFunctional.Common.Common;
using Microsoft.EntityFrameworkCore;

namespace AssignmateFunctional.API.Business;

public class UserManagementService(IServiceDao<AssignmateUser> _userDao)
	: IUserManagementService
{
	public async Task<Guid> RegisterAsync(RegisterUserDto registerUserDto)
	{

		UserRoles userRole = Enum.TryParse(registerUserDto.Role, ignoreCase: true, out UserRoles role)
					? role
					: throw new BadHttpRequestException($"Role: {registerUserDto.Role} is invalid");

		bool exists = await _userDao
			.Query()
			.AnyAsync(p => p.Email == registerUserDto.Email
			&& p.Role == userRole
			&& p.FirstName == registerUserDto.Name);

		if (exists)
		{
			throw new BadHttpRequestException("User already exists");
		}


		AssignmateUser assignmateUser = new()
		{
			Email = registerUserDto.Email,
			Password = PasswordHelper.Hash(registerUserDto.Password),
			FirstName = registerUserDto.Name,
			PhoneNumber = registerUserDto.Phone,
			Role = userRole,
		};

		_ = await _userDao.AddAsync(assignmateUser);

		int affectedRows = await _userDao.SaveChangesAsync();

		return affectedRows > 0
			? assignmateUser.Id
			: throw new InvalidOperationException("Something went wrong while saving to DB");

	}
}
