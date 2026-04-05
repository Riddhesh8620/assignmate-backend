using AssignmateFunctional.API.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmateFunctional.API.Entities;

public class AssignmateUser : BaseEntity
{
    [Column(TypeName = "varchar(200)")]
    public required string FirstName { get; set; }
    public string LastName { get; set; }
    [Column(TypeName = "varchar(100)")]
    public required string Email { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string UserName { get; set; } = $"User-{Guid.CreateVersion7()}";
    public required string Password { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string PhoneNumber { get; set; }
    public UserRoles Role { get; set; }
}