using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Issues
{
    public class CreateIssueDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int AreaId { get; set; }
        public int ProjectId { get; set; }
        public string CreatedByUserId { get; set; }
        public string? AssignedToUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Priority Priority { get; set; }
        public List<int> LabelIds { get; set; }
        public List<RevitElementDto> RevitElements { get; set; }
    }
}
