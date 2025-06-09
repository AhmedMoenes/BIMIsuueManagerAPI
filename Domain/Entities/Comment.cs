namespace Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Message { get; set; }
        public int IssueId { get; set; }
        public Issue Issue { get; set; }
        public int? SnapshotId { get; set; }
        public Snapshot? Snapshot { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByUserId { get; set; }
        public User CreatedByUser { get;set; }
    }
}
