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

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateIssueDto dto)
        {
            IssueDto result = await _issueService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { id = result.IssueId },
                result);
        }

        [HttpPut("edit/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateIssueDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _issueService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _issueService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetByProjectId(int projectId)
        {
            IEnumerable<IssueDto> issues = await _issueService.GetByProjectIdAsync(projectId);
            return Ok(issues);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetByUserId(string userId)
        {
            IEnumerable<IssueDto> issues = await _issueService.GetByUserIdAsync(userId);
            return Ok(issues);
        }

        [HttpGet("resolved")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetResolved()
        {
            var issues = await _issueService.GetResolvedIssuesAsync();
            return Ok(issues);
        }

        [HttpGet("unresolved")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetUnresolved()
        {
            var issues = await _issueService.GetUnresolvedIssuesAsync();
            return Ok(issues);
        }

        [HttpGet("deleted")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetDeleted()
        {
            var issues = await _issueService.GetDeletedIssuesAsync();
            return Ok(issues);
        }

        [HttpPut("resolve/{id}")]
        public async Task<IActionResult> MarkAsResolved(int id)
        {
            bool success = await _issueService.MarkAsResolvedAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> Restore(int id)
        {
            bool success = await _issueService.RestoreAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
