namespace AssignmateFunctional.API.DAL.Services;

public interface IAuditScope
{
	Guid GetUserId();
	Guid GetRoleId();
    void SetUserId(Guid userId);
    void SetRoleId(Guid roleId);
}