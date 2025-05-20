using System.Security.Claims;
using Application.DTOs;
using Application.DTOs.Projects;
using Application.Interfaces;
using Application.Services;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        //Get All Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
        {
            IEnumerable<ProjectDto> projects = await _projectService.GetAllAsync();
            return Ok(projects);
        }

        //Get Project By Id
        
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

        //Create Project
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProjectDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _projectService.CreateAsync(dto);
            // To Be Edited
            return StatusCode(201);
        }

        //UpdateAsync Project
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

        //DeleteAsync Project
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
        [Authorize(Roles = UserRoles.Admin)]
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

