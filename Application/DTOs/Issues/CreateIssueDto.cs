namespace Application.DTOs.Issues
{
    public class CreateIssueDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<string> Comments { get; set; }
        public ICollection<IssueLabel> Labels { get; set; }
        
        public string? AssignedToUserId { get; set; }

        public int ProjectId { get; set; }
        public string CreatedByUserId { get; set; }

    }
}
