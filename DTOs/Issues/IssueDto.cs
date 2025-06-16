using DTOs.Areas;
using DTOs.Comments;
using DTOs.Labels;
using DTOs.RevitElements;
using DTOs.Snapshots;

namespace DTOs.Issues
{
    public class IssueDto
    {
        public int IssueId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public Priority Priority { get; set; } 

        public DateTime CreatedAt { get; set; }
        public string CreatedByUser { get; set; }
        public string? AssignedToUser { get; set; }
        public string? AssignedToUserId { get; set; }
        public string ProjectName { get; set; }

        public bool IsResolved { get; set; } 
        public bool IsDeleted { get; set; }

        public AreaDto Area { get; set; }

        public List<LabelDto> Labels { get; set; } = new();
        public List<RevitElementDto> RevitElements { get; set; } = new();
        public List<CommentDto> Comments { get; set; } = new();
        public SnapshotDto? Snapshot { get; set; }

        public string? ImagePath => Snapshot?.Path;
    }
}
