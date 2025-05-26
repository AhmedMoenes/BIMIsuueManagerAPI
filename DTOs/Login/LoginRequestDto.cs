using System.ComponentModel.DataAnnotations;

namespace DTOs.Login
{
    public class LoginRequestDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
