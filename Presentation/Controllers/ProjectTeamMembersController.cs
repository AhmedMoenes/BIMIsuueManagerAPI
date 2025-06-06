

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

        [HttpGet("team-project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectTeamMemberDto>>> GetByProjectId(int projectId)
        {
            IEnumerable<ProjectTeamMemberDto> members = await _service.GetByProjectIdAsync(projectId);
            return Ok(members);
        }

        [HttpGet("team-user/{userId}")]
        public async Task<ActionResult<int>> GetTeamByUserId(string userId)
        {
            IEnumerable<ProjectTeamMemberDto> memberships = await _service.GetByUserIdAsync(userId);
            return Ok(memberships);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<int>> GetByUserId(string userId)
        {
            IEnumerable<ProjectTeamMemberDto> memberships = await _service.GetByUserIdAsync(userId);

            var firstProjectId = memberships?.FirstOrDefault()?.ProjectId ?? 0;

            if (firstProjectId == 0)
                return NotFound("Project not found for this user.");

            return Ok(firstProjectId);
        }

        [HttpPost("user-project")]
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
        public async Task<IActionResult> RemoveUserFromProject(int projectId,  string userId)
        {
            var removed = await _service.RemoveAsync(projectId, userId);
            if (!removed)
                return NotFound();

            return NoContent();
        }

    }
}
