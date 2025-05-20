using Application.DTOs.Comments;
using Application.Interfaces;
using Application.Services;
using System;

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
            var comment = await _commentService.GetByIdAsync(id);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CommentDto dto)
        {
            await _commentService.CreateAsync(dto);
            return Created("",dto); ////////////////////////////????????????????????
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
