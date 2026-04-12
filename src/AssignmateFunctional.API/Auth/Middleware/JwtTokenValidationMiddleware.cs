using AssignmateFunctional.API.Auth.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AssignmateFunctional.API.Auth.Middleware;

public class JwtTokenValidationMiddleware(
	ILogger<JwtTokenValidationMiddleware> logger,
	IJwtTokenService jwtTokenService)
	: IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		if (!(context.Request.Path.StartsWithSegments("/auth/login")
			|| context.Request.Path.StartsWithSegments("auth/register")))
		{
			await next(context);
		}

		string authHeader = context.Request.Headers.Authorization.ToString();
		string token = authHeader["Bearer ".Length..].Trim();

		try
		{
			ClaimsPrincipal? principal = jwtTokenService.ValidateToken(token);
			context.User = principal;
		}
		catch (SecurityTokenExpiredException ex)
		{
			logger.LogWarning($"Rejected expired token: {ex.Message}");
			await WriteUnauthorized(context, "token_expired");
		}
		catch (SecurityTokenException ex)
		{
			logger.LogWarning("Rejected invalid token: {Message}", ex.Message);
			await WriteUnauthorized(context, "invalid_token");
		}
	}

	private static async Task WriteUnauthorized(HttpContext context, string error)
	{
		context.Response.StatusCode = 401;
		context.Response.ContentType = "application/json";
		context.Response.Headers.WWWAuthenticate =
			$"Bearer error=\"{error}\"";
		await context.Response.WriteAsJsonAsync(new { error });
	}
}
