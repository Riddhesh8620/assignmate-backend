using AssignmateFunctional.API.Business;
using AssignmateFunctional.Common.Common;
using Microsoft.AspNetCore.Mvc;

namespace AssignmateFunctional.API.Controllers;

[ApiController]
public class AuthController(IUserManagementService userManagementService) : BaseController
{
	[HttpPost("create-user")]
	public async Task<IActionResult> CreateUserAsync([FromBody] RegisterUserDto registerUserDto)
	{
		return Ok(await userManagementService.RegisterAsync(registerUserDto));
	}
}
