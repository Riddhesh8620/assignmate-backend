using AssignmateFunctional.API.Entities;
using AssignmateFunctional.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmateFunctional.DAL.Entities;

public class Assignment : BaseEntity
{
	[ForeignKey(nameof(AssignmentOwner))]
	public Guid UserId { get; set; }
	[Column(TypeName = "TEXT")]
	public string Title { get; set; } = default!;
	[Column(TypeName = "TEXT")]
	public string Subject { get; set; } = default!;
	[Column(TypeName = "TEXT")]
	public string Topic { get; set; } = default!;
	public string? Description { get; set; }
	public uint NumPages { get; set; } = 1;
	[Column(TypeName = "TIMESTAMPTZ")]
	public DateTime Deadline { get; set; }
	public double Budget { get; set; }
	public string? SpecialInstructions { get; set; }
	public AssignmentStatus Status { get; set; } = AssignmentStatus.open;
	public Guid? CollegeId { get; set; }
	public string? FileName { get; set; }
	public byte[]? FileContent { get; set; }
	public virtual AssignmateUser AssignmentOwner { get; set; } = null!;
}