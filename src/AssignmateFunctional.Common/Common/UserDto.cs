
namespace AssignmateFunctional.Common.Common;

public sealed class RegisterUserDto
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string? College { get; set; }
}