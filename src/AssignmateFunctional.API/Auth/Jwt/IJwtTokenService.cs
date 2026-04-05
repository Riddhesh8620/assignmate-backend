namespace AssignmateFunctional.API.Auth.Jwt;

public interface IJwtTokenService
{
	string GenerateToken(string userId, string username, IEnumerable<string> roles);
}
