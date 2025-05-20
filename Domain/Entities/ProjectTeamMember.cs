namespace Domain.Entities
{
    public class ProjectTeamMember
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Role { get; set; }
    }
}
