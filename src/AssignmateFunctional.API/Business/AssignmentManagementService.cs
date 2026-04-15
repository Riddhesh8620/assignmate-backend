using AssignmateFunctional.API.DAL.DAO;
using AssignmateFunctional.API.DAL.Services;
using AssignmateFunctional.Common.DTO;
using AssignmateFunctional.DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace AssignmateFunctional.API.Business;

public class AssignmentManagementService(
	ISessionDao _sessionDao,
	IAuditScope _auditScope,
	IServiceDao<Assignment> _assignmentDao,
	ILogger<AssignmentManagementService> Logger
	) : IAssignmentManagementService
{

	public async Task<Guid> SaveUserAssignment(
		AddUserAssignmentDto addUserAssignmentDto,
		CancellationToken cancellationToken = default)
	{
		try
		{
			await _sessionDao.BeginTransactionAsync();

			DateTime current = DateTime.UtcNow;

			Assignment newAssignment = new()
			{
				Id = Guid.CreateVersion7(),
				/* Audit Details  */
				AddedBy = _auditScope.GetUserId(),
				UpdatedBy = _auditScope.GetUserId(),
				AddedOn = current,
				UpdatedOn = current,
				UserId = _auditScope.GetUserId(),
				/*Assignment Details */
				Budget = addUserAssignmentDto.budget,
				Deadline = DateTime.SpecifyKind(addUserAssignmentDto.deadline, DateTimeKind.Utc),
				Description = addUserAssignmentDto.description,
				Title = addUserAssignmentDto.title,
				Subject = addUserAssignmentDto.subject,
				Topic = addUserAssignmentDto.topic,
				NumPages = addUserAssignmentDto.num_pages,
				SpecialInstructions = addUserAssignmentDto.special_instructions,
			};


			if (addUserAssignmentDto.file != null || addUserAssignmentDto.file?.Length != 0)
			{
				using MemoryStream stream = new();
				await addUserAssignmentDto.file!.CopyToAsync(stream, cancellationToken);
				byte[] fileContent = stream.ToArray();
				newAssignment.FileName = addUserAssignmentDto.file.FileName;
				newAssignment.FileContent = fileContent;
			}

			_ = await _sessionDao.AddAsync(newAssignment);

			_ = await _sessionDao.CommitAsync();
			return newAssignment.Id;
		}
		catch (DbUpdateException ex)
		{

			Logger.LogWarning(ex.Message);
			await _sessionDao.RollbackAsync();
			throw;
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, ex.Message);
			throw;
		}
	}


	public async Task<AssignmentDashboardDto> GetAssignmentDetailsForDashboardById(Guid assignmentId)
	{
		Assignment assignment = await _assignmentDao.GetByIdAsync(assignmentId)
			?? throw new InvalidDataException($"Assignment not found for ID:{assignmentId}");

		AssignmentDashboardDto assignmentDashboardDto = GetAssignmentDashboardDto(assignment);
		return assignmentDashboardDto;
	}

	public async Task<IEnumerable<AssignmentDashboardDto>> GetAssignmentsForDashboards(CancellationToken cancellationToken)
	{
		IEnumerable<AssignmentDashboardDto> assignmentDashboardDtoList = await _assignmentDao
			.Query(false)
			.Where(a => a.UserId == _auditScope.GetUserId())
			.Select(assignment => GetAssignmentDashboardDto(assignment))
			.ToListAsync(cancellationToken);

		return assignmentDashboardDtoList;
	}

	public async Task<(byte[]? FileContent, string? FileName)> GetAssignmentDocument(Guid assignmentId)
	{
		Assignment assignment = await _assignmentDao.GetByIdAsync(assignmentId)
			?? throw new InvalidDataException($"Assignment not found for ID:{assignmentId}");

		if (!string.IsNullOrEmpty(assignment.FileName)
			&& assignment.FileContent != null
			&& assignment.FileContent.Length != 0)
		{
			return (assignment.FileContent, assignment.FileName);
		}
		return (null, null);
	}

	private static AssignmentDashboardDto GetAssignmentDashboardDto(Assignment assignment)
	{
		AssignmentDashboardDto dto = new()
		{
			id = assignment.Id,
			budget = assignment.Budget,
			deadline = assignment.Deadline,
			description = assignment.Description,
			num_pages = assignment.NumPages,
			special_instructions = assignment.SpecialInstructions,
			status = $"{assignment.Status}",
			subject = assignment.Subject,
			title = assignment.Title,
			user_id = assignment.UserId,
			file_url = assignment.FileName,
		};
		return dto;
	}
}