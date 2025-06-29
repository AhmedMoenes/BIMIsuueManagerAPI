namespace DTOs.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
        public int CompanyId { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        // public List<ProjectTeamMemberDto>? ProjectMemberships { get; set; }
    }
}
