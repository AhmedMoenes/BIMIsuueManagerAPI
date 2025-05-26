namespace DTOs.Comments
{
    public class CreateCommentDto
    {
        public string Message { get; set; }
        public int IssueId { get; set; }
        public string CreatedByUserId { get; set; }
    }
}
