using AssignmateFunctional.API.Business;
using AssignmateFunctional.Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmateFunctional.API.Controllers;

[ApiController]
[Route("assignments")]
public class AssignmentsController(
	IAssignmentManagementService _assignmentManagementService
	) : ControllerBase
{

	[HttpPost("save")]
	public async Task<IActionResult> CreateAssignmentAsync([FromForm] AddUserAssignmentDto addUserAssignmentDto, CancellationToken cancellationToken)
	{
		try
		{
			return Ok(new { Data = await _assignmentManagementService.SaveUserAssignment(addUserAssignmentDto, cancellationToken) });
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpGet("my")]
	public async Task<IActionResult> GetAssignmentsForDashboardAsync(CancellationToken cancellationToken)
	{
		return Ok(
			new
			{
				assignments = await _assignmentManagementService.GetAssignmentsForDashboards(cancellationToken)
			});
	}

	[HttpGet("get-document/{assignmentId}")]
	[AllowAnonymous]
	public async Task<IActionResult> GetAssignmentDocumentAsync(Guid assignmentId)
	{
		(byte[]? FileContent, string? FileName)? fileData = await _assignmentManagementService.GetAssignmentDocument(assignmentId);

		if (fileData == null)
		{
			return NotFound("File data is missing.");
		}

		// This returns the file directly to the browser
		return File(fileData.Value!.FileContent!, "application/octet-stream", fileData.Value.FileName);
	}
}