using Microsoft.AspNetCore.Http;

namespace AssignmateFunctional.Common.DTO;

public class AddUserAssignmentDto
{

    public string title { get; set; }

    public string subject { get; set; }

    public string topic { get; set; }

    public string? description { get; set; }

    public uint num_pages { get; set; }

    public DateTime deadline { get; set; }

    public double budget { get; set; }

    public string? special_instructions { get; set; }

    public IFormFile? file { get; set; }
}
