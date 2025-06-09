namespace DTOs.Comments
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedBy { get; set; }
        public int? SnapshotId { get; set; }
    }
}
