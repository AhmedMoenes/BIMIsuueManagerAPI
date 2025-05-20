using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;

namespace Presentation.Controllers
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

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AreaDto dto)
        {
            await _areaService.CreateAsync(dto);
            return Created("", dto); ////////////////////////////????????????????????
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
    }
}
