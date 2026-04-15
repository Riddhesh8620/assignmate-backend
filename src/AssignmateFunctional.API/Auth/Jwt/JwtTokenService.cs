using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssignmateFunctional.API.Auth.Jwt;

/// <inheritdoc/>
public sealed class JwtTokenService(
	IOptions<JwtOptions> options)
	: IJwtTokenService
{
	private readonly JwtOptions _options = options.Value;

	/// <inheritdoc/>
	public string GenerateToken(Guid userId, string roles)
	{
		List<Claim> claims =
	[
		new(JwtRegisteredClaimNames.Sub, userId.ToString()),
		new(TokenConstants.UserId, userId.ToString()),
		new(TokenConstants.RoleId, roles),
		new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
	];

		SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_options.SecretKey));
		SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

		JwtSecurityToken jwtSecurityToken = new(
			issuer: _options.Issuer,
			audience: _options.Audience,
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
			signingCredentials: creds
		);
		string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
		return token;
	}

	public ClaimsPrincipal? ValidateToken(string token)
	{
		if (string.IsNullOrWhiteSpace(token))
		{
			return null;
		}

		JwtSecurityTokenHandler tokenHandler = new();
		SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_options.SecretKey));

		try
		{
			ClaimsPrincipal principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = symmetricSecurityKey,

				ValidateIssuer = true,
				ValidIssuer = _options.Issuer,

				ValidateAudience = true,
				ValidAudience = _options.Audience,

				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero // Removes the default 5-minute grace period
			}, out SecurityToken validatedToken);

			return principal;
		}
		catch
		{
			// Token validation failed (expired, tampered, wrong issuer, etc.)
			return null;
		}
	}
}

internal static class TokenConstants
{
	internal static string UserId = "user_id";
	internal static string RoleId = "role_id";

}