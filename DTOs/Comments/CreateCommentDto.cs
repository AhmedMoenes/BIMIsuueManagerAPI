using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Comments
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public int IssueId { get; set; }
        public string CreatedByUserId { get; set; }
    }
}
