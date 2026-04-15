using System.Security.Claims;

namespace AssignmateFunctional.API.Auth.Jwt;

public interface IJwtTokenService
{
	string GenerateToken(Guid userId, string roles);
	ClaimsPrincipal? ValidateToken(string token);
}
