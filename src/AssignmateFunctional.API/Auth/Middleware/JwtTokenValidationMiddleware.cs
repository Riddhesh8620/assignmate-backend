using AssignmateFunctional.API.Auth.Jwt;
using AssignmateFunctional.API.DAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AssignmateFunctional.API.Auth.Middleware;

public class JwtTokenValidationMiddleware(
	ILogger<JwtTokenValidationMiddleware> _logger,
	IJwtTokenService _jwtTokenService,
	IAuditScope _auditScope)
	: IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		Endpoint? endpoint = context.GetEndpoint();

		if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
		{
			await next(context);
			return;
		}
		else
		{
			string token = context.Request.Cookies.TryGetValue("Bearer", out string? tokenFromCookie)
				? tokenFromCookie
				: throw new BadHttpRequestException("Token missing");
			try
			{
				ClaimsPrincipal? principal = _jwtTokenService.ValidateToken(token);
				context.User = principal;
				string? userIdClaim = principal?.FindFirstValue(TokenConstants.UserId);

				string? roleClaim = principal?.FindFirstValue(TokenConstants.RoleId);

				if (!string.IsNullOrEmpty(userIdClaim) && !string.IsNullOrEmpty(roleClaim))
				{
					// Set Audit Context
					_auditScope.SetUserId(Guid.Parse(userIdClaim));

					// Pass the list directly to your audit scope
					_auditScope.SetRoleId(roleClaim);
				}
				else
				{
					_logger.LogWarning("Authentication failed: Missing User ID or Roles in claims.");
					throw new SecurityTokenException("Invalid claim set");
				}
				await next(context);
			}
			catch (SecurityTokenExpiredException ex)
			{
				_logger.LogWarning($"Rejected expired token: {ex.Message}");
				await WriteUnauthorized(context, "token_expired");
			}
			catch (SecurityTokenException ex)
			{
				_logger.LogWarning("Rejected invalid token: {Message}", ex.Message);
				await WriteUnauthorized(context, "invalid_token");
			}
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
