using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.IssueLabel
{
    public class AssignLabelToIssueDto
    {
        public int IssueId { get; set; }
        public int LabelId { get; set; }
    }
}
