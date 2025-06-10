namespace DTOs.Snapshots
{
    public class SnapshotDto
    {
        public int SnapshotId { get; set; }
        public string Path { get; set; } = string.Empty;
        //public string ImagePath => $"{Path}";
        public DateTime CreatedAt { get; set; }
        //public string? LocalImagePath { get; set; }
    }
}
