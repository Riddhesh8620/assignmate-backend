using AssignmateFunctional.Common.DTO;

namespace AssignmateFunctional.API.Business;

public interface IAssignmentManagementService
{
	Task<(byte[]? FileContent, string? FileName)> GetAssignmentDocument(Guid assignmentId);
	Task<IEnumerable<AssignmentDashboardDto>> GetAssignmentsForDashboards(CancellationToken cancellationToken);
	Task<Guid> SaveUserAssignment(AddUserAssignmentDto addUserAssignmentDto, CancellationToken cancellationToken = default!);
}
