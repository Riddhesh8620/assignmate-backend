using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssignmateFunctional.API.Auth.Jwt;

/// <inheritdoc/>
public sealed class JwtTokenService(IOptions<JwtOptions> options) : IJwtTokenService
{
	private readonly JwtOptions _options = options.Value;

	/// <inheritdoc/>
	public string GenerateToken(string userId, string username, IEnumerable<string> roles)
	{
		List<Claim> claims =
	[
		new(JwtRegisteredClaimNames.Sub, userId),
		new(JwtRegisteredClaimNames.UniqueName, username),
		new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		.. roles.Select(role => new Claim(ClaimTypes.Role, role)),
	];

		SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_options.SecretKey));
		SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

		JwtSecurityToken token = new(
			issuer: _options.Issuer,
			audience: _options.Audience,
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}