using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AssignmateFunctional.API.Auth.Jwt;

public static class JwtExtensions
{
	public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration config)
	{
		_ = services.Configure<JwtOptions>(config.GetSection("Jwt"));

		JwtOptions jwtOptions = config.GetSection("Jwt").Get<JwtOptions>()!;

		byte[] key = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);

		_ = services.AddAuthentication("Bearer")
			.AddJwtBearer("Bearer", options =>
			{
				options.RequireHttpsMetadata = true;

				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = true,

					ValidIssuer = jwtOptions.Issuer,
					ValidAudience = jwtOptions.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(key),

					ClockSkew = TimeSpan.Zero // avoid extra expiry delay
				};
			});

		_ = services.AddScoped<IJwtTokenService, JwtTokenService>();

		return services;
	}
}
