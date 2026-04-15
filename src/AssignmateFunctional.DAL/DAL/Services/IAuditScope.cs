namespace AssignmateFunctional.API.DAL.Services;

public interface IAuditScope
{
	Guid GetUserId();
    string GetRoleId();
    void SetUserId(Guid userId);
    void SetRoleId(string roleId);
}