namespace DTOs.Snapshots
{
    public class SnapshotDto
    {
        public int SnapshotId { get; set; }
        public string Path { get; set; } = string.Empty;
        public string ImagePath => $"https://localhost:44374/{Path}";
        public DateTime CreatedAt { get; set; }
        public string? LocalImagePath { get; set; }
    }
}
