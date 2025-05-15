namespace Domain.Entities
{
    public class Issue
    {
        public int IssueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
        public string? AssignedToUserId { get; set; }
        public User AssignedToUser { get; set; }
        public ICollection<RevitElement> RevitElements { get; set; }
        public ICollection<IssueLabel> Labels { get; set; }
    }
}
