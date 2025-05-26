using DTOs.Companies;

namespace DTOs.Projects
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<CompanySummaryDto> Companies { get; set; }
    }
}
