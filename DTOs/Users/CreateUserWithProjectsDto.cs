namespace DTOs.Users
{
    public class CreateUserWithProjectsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
        public string Role { get; set; } 

        public List<ProjectAssignmentDto> ProjectAssignments { get; set; } = new();
    }

    public class ProjectAssignmentDto
    {
        public int ProjectId { get; set; }
        public string RoleInProject { get; set; }
    }
}
