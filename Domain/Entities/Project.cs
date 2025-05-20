namespace Domain.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<ProjectTeamMember> ProjectTeamMembers { get; set; }
        public ICollection<Issue> Issues { get; set; }
        public ICollection<Label> Labels { get; set; }
        public ICollection<Area> Areas { get; set; }
    }
}
