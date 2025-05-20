using Application.DTOs;
using Application.DTOs.Users;
using Application.Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterUserAsync(dto);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

           
            var user = await _authService.GetUserByUsernameAsync(dto.UserName);

            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CompanyId = user.CompanyId
            };

            return CreatedAtAction(
                actionName: nameof(UsersController.GetById),
                controllerName: "Users",
                routeValues: new { id = user.Id },
                value: userDto
            );
        }

      
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isValid = await _authService.ValidateUserAsync(dto);
            if (!isValid)
                return Unauthorized(new { Message = "Invalid credentials" });

            var token = await _authService.CreateTokenAsync();
            return Ok(new { Token = token });
        }

       
      
    }
}
