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
        
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserOverviewDto>>> GetAll()
        {
            IEnumerable<UserOverviewDto> users = await _userService.GetUsersOverviewAsync();
            return Ok(users);
        }
        
        [HttpGet("company-users/{companyId}")]
        public async Task<ActionResult<IEnumerable<UserOverviewDto>>> GetCompanyUsers(int companyId)
        {
            IEnumerable<CompanyUserDto> users = await _userService.GetCompanyUsers(companyId);
            return Ok(users);
        }


        [HttpGet ("{id:alpha}")]
        public async Task<ActionResult<UserOverviewDto>> GetById(string id )
        {
            UserOverviewDto user = await _userService.GetByIdAsync(id);
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
        [Authorize(UserRoles.CompanyAdmin)]
        public async Task<ActionResult> CreateUserWithProjects([FromBody] CreateUserWithProjectsDto dto)
        {
            var adminUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(adminUserId))
                return Unauthorized();

            var result = await _userService.CreateUserWithProjectsAsync(adminUserId, dto);
            return Ok(result);
        }
        
        [HttpPut("edit/{id:alpha}")]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.UpdateAsync(id, dto);
            return NoContent();
        }

        
        [HttpDelete("delete/{id:alpha}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("get-id-by-email/{email}")]
        public async Task<ActionResult<string>> GetUserIdByEmail(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            if (user == null) return NotFound("User not found");
            return Ok(user.Id);
        }

        [HttpGet("project-users/{projectId}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByProject(int projectId)
        {
            var users = await _userService.GetByProjectIdAsync(projectId);
            return Ok(users);
        }


    }
}

