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
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            IEnumerable<UserDto> users = await _userService.GetAllAsync();
            return Ok(users);
        }

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

        //Review This Method For RegisterUserDto 
        [HttpPost ("register")]
        public async Task<ActionResult> Create([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDto createdUser = await _userService.RegisterAsync(dto);
            return CreatedAtAction(
                nameof(GetById),
                controllerName:"Users",
                routeValues: new{ id = createdUser.Id },
                value: createdUser
            );

        }

        [HttpPost("register-with-project")]
        [Authorize(UserRoles.SuperAdmin)]
        public async Task<ActionResult> CreateUserWithProjects([FromBody] CreateUserWithProjectsDto dto)
        {
            var adminUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(adminUserId))
                return Unauthorized();

            var result = await _userService.CreateUserWithProjectsAsync(adminUserId, dto);
            return Ok(result);
        }
        
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

        
        [HttpDelete("{id:alpha}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}

