using System.Text.Json.Serialization;

namespace AssignmateFunctional.Common.DTO
{
    public class LoginDto
    {
        [JsonPropertyName("email")]
        public required string EmailId { get; set; }
        public required string Password { get; set; }
    }
}