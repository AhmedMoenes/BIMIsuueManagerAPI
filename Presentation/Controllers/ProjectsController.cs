using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
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





    }
}

