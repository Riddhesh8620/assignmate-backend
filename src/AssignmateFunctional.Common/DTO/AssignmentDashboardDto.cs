namespace AssignmateFunctional.Common.DTO;

public class AssignmentDashboardDto
{
    public Guid id { get; set; }
    public Guid user_id { get; set; }
    public string status { get; set; }
    public string title { get; set; }
    public string subject { get; set; }
    public DateTime deadline { get; set; }
    public string? description { get; set; }
    public string? special_instructions { get; set; }
    public double budget { get; set; }
    public string? file_url { get; set; }
    public uint num_pages { get; set; }
    public string? writer_name { get; set; }
    public Guid writer_id { get; set; }

}
