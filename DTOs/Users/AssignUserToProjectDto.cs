namespace DTOs.Users
{
    public class AssignUserToProjectDto
    {
        public List<int> ProjectIds { get; set; }
        public string UserId { get; set; }
        public string Role { get; set; }
    }
}
