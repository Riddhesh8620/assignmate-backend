using AssignmateFunctional.API.DAL.DAO;
using AssignmateFunctional.Common.DTO;
using AssignmateFunctional.DAL.Entities;
using System.Security.Claims;

namespace AssignmateFunctional.API.Business;

public class AssignmentManagementService(
	IServiceDao<Assignments> serviceDao,
	IHttpContextAccessor httpContextAccessor)

	: IAssignmentManagementService
{

	//public IList<AssignmentDashboardDto> GetMyAssignments()
	//{
	//	//IEnumerable<Claim> UserId = httpContextAccessor.HttpContext.User.Claims;
	//}
}