namespace AssignmateFunctional.Common.DTO;

public class AssignmentDashboardDto
{
    public string title { get; set; }
    public string subject { get; set; }
    public string? description { get; set; }
    public double budget { get; set; }
    public string? file_url { get; set; }
    public uint num_pages { get; set; }
}
