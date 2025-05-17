using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetAll()
        {
          IEnumerable<IssueDto> issues = await _issueService.GetAllAsync();
            return Ok(issues);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IssueDto>> GetById(int id)
        {
            IssueDto issue = await _issueService.GetByIdAsync(id);
            if (issue is null)
            {
                return NotFound();
            }
            return Ok(issue);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateIssueDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _issueService.CreateAsync(dto);
            // To Be Edited
            return StatusCode(201);
        }

        //UpdateAsync Issue
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateIssueDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            await _issueService.UpdateAsync(id, dto);
            return NoContent();
        }

        //DeleteAsync Issue
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _issueService.DeleteAsync(id);
            return NoContent();
        }

    }
}
