namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueLabelsController : ControllerBase
    {
        private readonly IIssueLabelService _issueLabelService;

        public IssueLabelsController(IIssueLabelService issueLabelService)
        {
            _issueLabelService = issueLabelService;
        }

        
        [HttpPost("")]
        public async Task<IActionResult> AssignLabel([FromBody] AssignLabelToIssueDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var assignedLabel = await _issueLabelService.AssignLabelToIssueAsync(dto);
            return CreatedAtAction(
                nameof(AssignLabel),
                new { labelId = dto.LabelId },
                assignedLabel
            );
            return StatusCode(201);
        }

        
        [HttpDelete("")]
        public async Task<IActionResult> RemoveLabel([FromQuery] int issueId, [FromQuery] int labelId)
        {
            await _issueLabelService.RemoveLabelFromIssueAsync(issueId, labelId);
            return NoContent();
        }
    }
}
