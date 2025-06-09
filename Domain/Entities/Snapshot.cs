namespace Domain.Entities
{
    public class Snapshot
    {
        public int Id { get; set; }
        public string Path { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int IssueId { get; set; }
        public Issue Issue { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
