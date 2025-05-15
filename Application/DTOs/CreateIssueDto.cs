namespace Application.DTOs
{
    public class CreateIssueDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public string CreatedByUserId { get; set; }
        public string AssignedToUserId { get; set; }
    }
}
