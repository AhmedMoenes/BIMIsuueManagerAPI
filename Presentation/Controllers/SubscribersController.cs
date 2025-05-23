using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;

        public SubscribersController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<SubscriberDto>>> GetAll()
        {
            IEnumerable<SubscriberDto> subscribers = await _subscriberService.GetAllAsync();
            return Ok(subscribers);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriberDto>> GetById(int id)
        {
            SubscriberDto subscriber = await _subscriberService.GetByIdAsync(id);
            if (subscriber == null)
            {
                return NotFound();
            }

            return Ok(subscriber);
        }

        
        [HttpPost("")]
        public async Task<ActionResult> Create([FromBody] CreateSubscriberDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdSubscriber = await _subscriberService.CreateAsync(dto);

            return CreatedAtAction(
                actionName: nameof(GetById),
                controllerName: "Subscribers",
                routeValues: new { id = createdSubscriber.SubscriberId },
                value: createdSubscriber
            );
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateSubscriberDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _subscriberService.UpdateAsync(id, dto);
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _subscriberService.DeleteAsync(id);
            return NoContent();
        }
    }
}
