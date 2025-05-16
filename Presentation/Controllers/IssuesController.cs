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

        //Get All Issues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetAkk()
        {
          IEnumerable<IssueDto> issues = await _issueService.GetAllAsync();
            return Ok(issues);
        }

        //Get Issue By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<IssueDto>> GetById(int id)
        {
            IssueDto issue = await _issueService.GetByIdAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            return Ok(issue);
        }

        //Create Issue
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

        //Update Issue
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

        //Delete Issue
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _issueService.DeleteAsync(id);
            return NoContent();
        }

    }
}
