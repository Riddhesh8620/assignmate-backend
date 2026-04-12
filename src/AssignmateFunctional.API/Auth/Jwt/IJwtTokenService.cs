using System.Security.Claims;

namespace AssignmateFunctional.API.Auth.Jwt;

public interface IJwtTokenService
{
	string GenerateToken(Guid userId, IEnumerable<string> roles);
	ClaimsPrincipal? ValidateToken(string token);
}
