namespace Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int IssueId { get; set; }
        public Issue Issue { get; set; }

    }
}
