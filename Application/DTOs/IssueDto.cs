namespace Application.DTOs
{
    public class IssueDto
    {
        public int IssueId {get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
    }
}
