using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Issues
{
    public class UpdateIssueDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedToUserId { get; set; }
    }
}
