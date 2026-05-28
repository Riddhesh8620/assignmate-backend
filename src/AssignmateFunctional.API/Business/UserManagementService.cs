using AssignmateFunctional.API.Auth.Jwt;
using AssignmateFunctional.API.DAL.DAO;
using AssignmateFunctional.API.DAL.Services;
using AssignmateFunctional.API.Entities;
using AssignmateFunctional.Common.DTO;
using AssignmateFunctional.Common.Enums;
using AssignmateFunctional.Common.Helpers;
using AssignmateFunctional.DAL.DAL.Factory;
using Microsoft.EntityFrameworkCore;

namespace AssignmateFunctional.API.Business;

public class UserManagementService(
	IServiceDao<AssignmateUser> _userDao,
	IAuditScope auditScope,
	IJwtTokenService jwtTokenService)
	: IUserManagementService
{
	public async Task<UserStoreDto> RegisterAsync(RegisterUserDto registerUserDto)
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
			Id = Guid.CreateVersion7(),
			AddedOn = DateTime.UtcNow,
			UpdatedOn = DateTime.UtcNow,
			AddedBy = auditScope.GetUserId(),
			UpdatedBy = auditScope.GetUserId(),
			Email = registerUserDto.Email,
			Password = PasswordHelper.BCryptHash(registerUserDto.Password),
			FirstName = registerUserDto.Name,
			PhoneISD = registerUserDto.PhoneISD,
			PhoneNumber = registerUserDto.Phone,
			Role = userRole,
		};

		_ = await _userDao.AddAsync(assignmateUser);

		int affectedRows = await _userDao.SaveChangesAsync();

		UserStoreDto userStoreDto = assignmateUser.GetUserStore();

		return affectedRows > 0
			? userStoreDto
			: throw new InvalidOperationException(
				"Something went wrong while saving to DB");
	}

	public async Task<UserStoreDto> HandleLoginAsync(LoginDto loginDto)
	{
		AssignmateUser user = await _userDao
			.Query()
			.FirstOrDefaultAsync(p =>
			p.Email == loginDto.EmailId, new CancellationToken())
			?? throw new UnauthorizedAccessException("User does not exists");


		bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
		UserStoreDto userStoreDto = user.GetUserStore();

		return !isPasswordValid
			? throw new UnauthorizedAccessException("Incorrect password entered.")
			: userStoreDto;
	}
}
