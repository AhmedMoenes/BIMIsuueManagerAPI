﻿namespace DTOs.Users
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int CompanyId { get; set; }
    }
}
