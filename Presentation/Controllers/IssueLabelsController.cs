using Application.DTOs;
using Application.DTOs.Issues;
using Application.Interfaces;

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

        //Assign a label to an issue 
        [HttpPost]
        public async Task<IActionResult> AssignLabel([FromBody] AssignLabelToIssueDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _issueLabelService.AssignLabelToIssueAsync(dto);
            return StatusCode(201);
        }

        //Remove a label from an issue
        [HttpDelete]
        public async Task<IActionResult> RemoveLabel([FromQuery] int issueId, [FromQuery] int labelId)
        {
            await _issueLabelService.RemoveLabelFromIssueAsync(issueId, labelId);
            return NoContent();
        }
    }
}
