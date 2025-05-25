using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.RevitElements
{
    public class RevitElementDto
    {
        public int RevitElementId { get; set; }
        public string ElementId { get; set; }
        public string ElementUniqueId { get; set; }
        public string ViewpointCameraPosition { get; set; }
        public string SnapshotImagePath { get; set; }
        public int IssueId { get; set; }
    }
}
