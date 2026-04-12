using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AssignmateFunctional.Common.DTO;

public class RegisterUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
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