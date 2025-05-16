using Application.DTOs;
using Application.Interfaces;

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


        //Assign User To Project 
        [HttpPost]
        public async Task<IActionResult> AssignUserToProject([FromBody] AssignUserToProjectDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AssignAsync(dto);
            return StatusCode(201);
        }

        //Remove User From Project
        [HttpDelete]
        public async Task<IActionResult> RemoveUserFromProject([FromQuery] int projectId, [FromQuery] string userId)
        {
            await _service.RemoveAsync(projectId, userId);
            return NoContent();
        }

    }
}
