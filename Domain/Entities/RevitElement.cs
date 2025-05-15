namespace Domain.Entities
{
    public class RevitElement
    {
        public int RevitElementId { get; set; }
        public string ElementId { get; set; }
        public string ElementUniqueId { get; set; }
        public string ViewpointCameraPosition { get; set; }
        public string SnapshotImagePath { get; set; }
        public int IssueId { get; set; }
        public Issue Issue { get; set; }
    }
}
