using Application.DTOs;
using Application.DTOs.User;
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

        //Get All Subscribers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriberDto>>> GetAll()
        {
            IEnumerable<SubscriberDto> subscribers = await _subscriberService.GetAllAsync();
            return Ok(subscribers);
        }

        //Get Subscriber By Id
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

        //Create Subscriber
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateSubscriberDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _subscriberService.CreateAsync(dto);
            // To Be Edited
            return StatusCode(201);
        }

        //UpdateAsync Subscriber
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

        //DeleteAsync Subscriber
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _subscriberService.DeleteAsync(id);
            return NoContent();
        }
    }
}
