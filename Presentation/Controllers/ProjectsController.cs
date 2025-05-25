using Application.DTOs.Projects;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public ProjectsController(IProjectService projectService, IUserService userService)
        {
            _projectService = projectService;
            _userService = userService;
        }

       
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
        {
            IEnumerable<ProjectDto> projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

       
        
        [HttpGet("{id}")]
        public async Task<ActionResult<IssueDto>> GetById(int id)
        {
            ProjectDto issue = await _projectService.GetByIdAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            return Ok(issue);
        }

        
        [HttpPost("")]
        public async Task<ActionResult> Create([FromBody] CreateProjectDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdProject = await _projectService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdProject.ProjectId },
                createdProject

            );
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateProjectDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _projectService.UpdateAsync(id, dto);
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
           
            await _projectService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("overview/subscriber")]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<IActionResult> GetForSubscriber()
        {
            var result = await _projectService.GetForSubscriberAsync();
            return Ok(result);
        }

        [HttpGet("overview/company")]
        [Authorize(Roles = UserRoles.CompanyAdmin)]
        public async Task<IActionResult> GetForCompany()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var companyId = await _userService.GetCompanyIdAsync(userId);
            var result = await _projectService.GetForCompanyAsync(companyId);
            return Ok(result);
        }

        [HttpGet("overview/user")]
        [Authorize]
        public async Task<IActionResult> GetForUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _projectService.GetForUserAsync(userId);
            return Ok(result);
        }




    }
}

