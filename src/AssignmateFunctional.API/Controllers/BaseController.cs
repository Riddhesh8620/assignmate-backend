using Microsoft.AspNetCore.Mvc;

namespace AssignmateFunctional.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseController
	: ControllerBase
{

	protected void TokenPasrses(string token)
	{
		Response.Headers.Authorization = $"Bearer {token}";
		Response.Cookies.Append(
			"Bearer",
			token,
			new CookieOptions
			{
				HttpOnly = true,
				Secure = false,
				Expires = DateTimeOffset.UtcNow.AddMinutes(10),
				SameSite = SameSiteMode.Lax
			});
	}
}