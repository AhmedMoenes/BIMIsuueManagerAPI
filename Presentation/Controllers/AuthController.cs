namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Login with email and password, returns JWT token and user info.
        /// </summary>
        /// 
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _userService.LoginAsync(dto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return Ok(new { message = "Logged out successfully." });
        }

    }
}
