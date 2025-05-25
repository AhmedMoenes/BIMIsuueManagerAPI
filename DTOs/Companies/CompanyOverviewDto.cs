using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Companies
{
    public class CompanyOverviewDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int UsersCount { get; set; }
        public int ProjectsCount { get; set; }
        public int IssuesCount { get; set; }
    }
}
