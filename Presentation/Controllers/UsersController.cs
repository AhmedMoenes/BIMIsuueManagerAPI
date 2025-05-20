using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //Get All Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            IEnumerable<UserDto> users = await _userService.GetAllAsync();
            return Ok(users);
        }

        //Get User By Id

        [HttpGet ("{id:alpha}")]
        public async Task<ActionResult<UserDto>> GetById(string id )
        {
            UserDto user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //Create User
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.RegisterAsync(dto);
            // To Be Edited
            return StatusCode(201);
        }

        //UpdateAsync User
        [HttpPut("{id:alpha}")]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.UpdateAsync(id, dto);
            return NoContent();
        }

        //DeleteAsync User
        [HttpDelete("{id:alpha}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}

