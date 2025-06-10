namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAll()
        {
            IEnumerable<CommentDto> comments = await _commentService.GetAllAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetById(int id)
        {
            CommentDto comment = await _commentService.GetByIdAsync(id);
            return Ok(comment);
        }

        [HttpPost("")]
        public async Task<ActionResult> Create([FromBody] CreateCommentDto dto)
        {
            CommentDto createdComment = await _commentService.CreateAsync(dto);
            return CreatedAtAction(

                 nameof(GetById),
                 new { id = createdComment.CommentId },
                 createdComment
            );
        }

        [HttpPost("issue/{issueId}/create")]
        public async Task<ActionResult> CreateForIssue(int issueId, [FromBody] CreateCommentDto dto)
        {
            CommentDto createdComment = await _commentService.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdComment.CommentId },
                createdComment);
        }

        [HttpGet("issue/{issueId}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetByIssue(int issueId)
        {
            IEnumerable<CommentDto> comments = await _commentService.GetByIssueIdAsync(issueId);
            return Ok(comments);
        }

        [HttpGet("snapshot/{snapshotId}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetBySnapshot(int snapshotId)
        {
            IEnumerable<CommentDto> comments = await _commentService.GetBySnapshotIdAsync(snapshotId);
            return Ok(comments);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CommentDto dto)
        {
            await _commentService.UpdateAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _commentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
