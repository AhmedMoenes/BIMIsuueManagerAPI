using Application.DTOs.Projects;
using Application.DTOs.Users;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTeamMembersController : ControllerBase
    {
        private readonly IProjectTeamMemberService _service;

        public ProjectTeamMembersController(IProjectTeamMemberService service)
        {
            _service = service;
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectTeamMemberDto>>> GetByProjectId(int projectId)
        {
            var members = await _service.GetByProjectIdAsync(projectId);
            return Ok(members);
        }



        [HttpPost("")]
        public async Task<IActionResult> AssignUserToProject([FromBody] AssignUserToProjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.AssignAsync(dto);

            return CreatedAtAction(
                nameof(GetByProjectId),
                controllerName: "ProjectTeamMembers",
                new { projectId = dto.ProjectId },
                result
            );
        }


        [HttpDelete("project/{projectId}/user/{userId}")]
        public async Task<IActionResult> RemoveUserFromProject( int projectId,  string userId)
        {
            var removed = await _service.RemoveAsync(projectId, userId);
            if (!removed)
                return NotFound();

            return NoContent();
        }

    }
}
