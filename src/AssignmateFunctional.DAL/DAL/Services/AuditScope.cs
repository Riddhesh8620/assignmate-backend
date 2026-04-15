namespace AssignmateFunctional.API.DAL.Services;

public class AuditScope : IAuditScope
{
    private Guid UserId;
    private string RoleId;

    public AuditScope() { }


    public string GetRoleId()
    {
        return RoleId;
    }

    public Guid GetUserId()
    {
        return UserId;
    }

    public void SetRoleId(string roleId)
    {
        RoleId = roleId;
    }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

}
