using DTOs.IssueLabel;
using DTOs.RevitElements;
using DTOs.Snapshots;

namespace DTOs.Issues
{
    public enum Priority
    {
        Minor,
        Normal,
        Urgent,
        Critical
    }

    public class CreateIssueDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int AreaId { get; set; }
        public int ProjectId { get; set; }

        public string CreatedByUserId { get; set; }
        public string? AssignedToUserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public Priority Priority { get; set; }

        public bool IsResolved { get; set; } = false; 

        public List<AssignLabelToIssueDto> Labels { get; set; }
        public List<IssueRevitElementDto> RevitElements { get; set; }

        public SnapshotDto Snapshot { get; set; }
    }
}
