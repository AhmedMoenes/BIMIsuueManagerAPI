using DTOs.Projects;

namespace DTOs.Companies
{
    public class CompanyDto
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<ProjectSummaryDto> Projects { get; set; }
    }
}
