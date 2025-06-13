using DTOs.IssueLabel;
using DTOs.Snapshots;

namespace DTOs.Issues
{
    public class UpdateIssueDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }

        public int AreaId { get; set; }
        public string AssignedToUserId { get; set; }

        public Priority Priority { get; set; }

        public bool IsResolved { get; set; }
        public bool IsDeleted { get; set; } = false; 

        public List<AssignLabelToIssueDto> Labels { get; set; }
        public SnapshotDto Snapshot { get; set; } 
    }
}
