﻿namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<AreaDto>>> GetAll()
        {
            IEnumerable<AreaDto> areas = await _areaService.GetAllAsync();
            return Ok(areas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AreaDto>> GetById(int id)
        {
            var area = await _areaService.GetByIdAsync(id);
            return Ok(area);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateAreaDto dto)
        {
            var createdArea= await _areaService.CreateAsync(dto);
            return CreatedAtAction(

                nameof(GetById),
                new { id = createdArea.AreaId },
                createdArea
            );
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] AreaDto dto)
        {
            await _areaService.UpdateAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _areaService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<AreaDto>>> GetAreasByProject(int projectId)
        {
            var areas = await _areaService.GetByProjectIdAsync(projectId);
            return Ok(areas);
        }
    }
}
