using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Projects
{
    public class CreateProjectDto
    {
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CompanyId { get; set; }
        public ICollection<string> TeamMemberUserIds { get; set; }
        public ICollection<int> LabelIds { get; set; }
        public ICollection<string> AreaNames { get; set; }
    }
}
