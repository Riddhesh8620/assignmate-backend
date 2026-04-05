namespace AssignmateFunctional.API.Entities;

public abstract class BaseEntity
{

	public Guid Id { get; set; }
	public DateTime AddedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
	public Guid AddedBy { get; set; }
	public Guid UpdatedBy { get; set; }
}
