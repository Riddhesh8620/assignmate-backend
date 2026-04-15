using AssignmateFunctional.Common.Enums;
using AssignmateFunctional.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AssignmateFunctional.API.Entities;

public class AssignmateUser : BaseEntity
{
    [Column(TypeName = "varchar(200)")]
    public required string FirstName { get; set; }
    [Column(TypeName = "varchar(200)")]
    public string? LastName { get; set; }
    [Column(TypeName = "varchar(100)")]
    public required string Email { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string UserName { get; set; } = $"User-{Guid.CreateVersion7()}";
    [JsonIgnore]
    public required string Password { get; set; }
    [Column(TypeName = "INT4")]
    public uint PhoneISD { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string PhoneNumber { get; set; } = default!;
    public UserRoles Role { get; set; }
    public JsonDocument? WriterProfile {  get; set; }
}