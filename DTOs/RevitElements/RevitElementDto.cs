namespace DTOs.RevitElements
{
    public class RevitElementDto
    {
        public int RevitElementId { get; set; }
        public string ElementId { get; set; }
        public string ElementUniqueId { get; set; }
        public string ViewpointCameraPosition { get; set; } 
        public string ViewpointForwardDirection { get; set; } 
        public string ViewpointUpDirection { get; set; } 
        public int IssueId { get; set; }
    }
}
