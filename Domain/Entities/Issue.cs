namespace Domain.Entities
{
    public enum Priority
    {
        Minor,
        Normal,
        Urgent,
        Critical
    }

    public class Issue
    {
        public int IssueId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public string CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public string? AssignedToUserId { get; set; }
        public User AssignedToUser { get; set; }

        public int AreaId { get; set; }
        public Area Area { get; set; }

        public DateTime CreatedAt { get; set; }

        public Priority Priority { get; set; }

        public bool IsResolved { get; set; } = false; 
        public bool IsDeleted { get; set; } = false;

        public ICollection<RevitElement>? RevitElements { get; set; }
        public ICollection<IssueLabel> Labels { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Snapshot> Snapshots { get; set; }
    }
}
