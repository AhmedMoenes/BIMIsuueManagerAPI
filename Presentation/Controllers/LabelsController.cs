using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelService _labelService;

        public LabelsController(ILabelService labelService)
        {
            _labelService = labelService;
        }

        // Get All Labels
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable <LabelDto> labels = await _labelService.GetAllAsync();
            return Ok(labels);
        }

        //Get Label By Id
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<LabelDto>> GetById(int id)
        {
            LabelDto label = await _labelService.GetByIdAsync(id);
            if (label == null)
                return NotFound();

            return Ok(label);
        }

        // Get Label By Name
        //[HttpGet("{name:alpha}")]
        //public async Task<ActionResult<LabelDto>> GetById(string name)
        //{
        //    LabelDto label = await _labelService.GetByIdAsync(name);
        //    if (label == null)
        //        return NotFound();

        //    return Ok(label);
        //}

        //Create Label
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateLabelDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _labelService.CreateAsync(dto);
            //To Be Edited
            return StatusCode(201);
        }

        //UpdateAsync label
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateLabelDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _labelService.UpdateAsync(id, dto);
            return NoContent();
        }

        //DeleteAsync Label
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _labelService.DeleteAsync(id);
            return NoContent();
        }
    }
}
