using Application.DTOs.RevitElements;
using Application.Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevitElementsController : ControllerBase
    {
        private readonly IRevitElementService _revitElementService;

        public RevitElementsController(IRevitElementService revitElementService)
        {
            _revitElementService = revitElementService;
        }

        //Get all Revit elements by issue ID
        [HttpGet("issue/{issueId}")]
        public async Task<ActionResult<IEnumerable<RevitElementDto>>> GetByIssueId(int issueId)
        {
            IEnumerable<RevitElementDto> elements = await _revitElementService.GetByIssueIdAsync(issueId);
            return Ok(elements);
        }


        //Adding Revit element 
        [HttpPost]
        public async Task<IActionResult> AddRevitElement([FromBody] RevitElementDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _revitElementService.AddAsync(dto);
            return StatusCode(201);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _revitElementService.DeleteAsync(id);
            return NoContent();
        }
    }
}
