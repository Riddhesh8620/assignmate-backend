using AssignmateFunctional.API.Auth.Jwt;
using AssignmateFunctional.API.Business;
using AssignmateFunctional.Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmateFunctional.API.Controllers;


[ApiController]
public class AuthController(
	IUserManagementService userManagementService,
	IJwtTokenService jwtTokenService) : BaseController
{
	[HttpPost("register")]
	[AllowAnonymous]
	public async Task<IActionResult> CreateUserAsync([FromBody] RegisterUserDto registerUserDto)
	{
		UserStoreDto result = await userManagementService.RegisterAsync(registerUserDto);
		string token = jwtTokenService.GenerateToken(result.id, result.role);
		TokenPasrses(token);
		return Ok(result);

	}

	[HttpPost("login")]
	[AllowAnonymous]
	public async Task<IActionResult> Login([FromBody] LoginDto login)
	{
		UserStoreDto user = await userManagementService.HandleLoginAsync(login);
		string token = jwtTokenService.GenerateToken(user.id, user.role);
		TokenPasrses(token);
		return Ok(user);

	}
}