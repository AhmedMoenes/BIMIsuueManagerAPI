namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnapshotController : ControllerBase
    {
        private readonly ISnapshotService _snapshotService;

        public SnapshotController(ISnapshotService snapshotService)
        {
            _snapshotService = snapshotService;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<string>> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid image file.");

            var path = await _snapshotService.UploadImageAsync(file);
            return Ok(path);
        }
    }
}
