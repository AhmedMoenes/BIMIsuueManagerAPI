using DTOs.IssueLabel;

namespace DTOs.Issues
{
    public class UpdateIssueDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AreaId { get; set; }
        public string AssignedToUserId { get; set; }
        public Priority Priority { get; set; }
        public List<AssignLabelToIssueDto> Labels { get; set; }

    }
}
