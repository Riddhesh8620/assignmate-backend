using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json.Serialization;

namespace AssignmateFunctional.Common.DTO;

public class RegisterUserDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("phone_isd")]
    public uint PhoneISD { get; set; } = 91;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? College { get; set; }
}

public class UserStoreDto
{
    public Guid id { get; set; }
    public required string name { get; set; }
    public required string email { get; set; }
    public required string role { get; set; }
    public string? college_id { get; set; }
}