namespace AssignmateFunctional.API.DAL.Services;

public class AuditScope : IAuditScope
{
    private Guid UserId;
    private Guid RoleId;

    public AuditScope() { }


    public Guid GetRoleId()
    {
        return RoleId;
    }

    public Guid GetUserId()
    {
        return UserId;
    }

    public void SetRoleId(Guid roleId)
    {
        RoleId = roleId;
    }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

}
