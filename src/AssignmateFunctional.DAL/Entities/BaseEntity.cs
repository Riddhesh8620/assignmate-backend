using System.Text.Json.Serialization;

namespace AssignmateFunctional.API.Entities;

public abstract class BaseEntity
{
    [JsonIgnore]
    public Guid Id { get; set; }
    [JsonIgnore]
    public DateTime AddedOn { get; set; }
    [JsonIgnore]
    public DateTime UpdatedOn { get; set; }
    [JsonIgnore]
    public Guid AddedBy { get; set; }
    [JsonIgnore]
    public Guid UpdatedBy { get; set; }
}
