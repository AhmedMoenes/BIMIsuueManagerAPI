namespace DTOs.Users
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
        public string Role { get; set; }
    }
}
