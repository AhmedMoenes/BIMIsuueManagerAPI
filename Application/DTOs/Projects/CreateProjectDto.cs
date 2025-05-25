using Application.DTOs.Areas;
using Application.DTOs.Labels;
    
namespace Application.DTOs.Projects
{
    public class CreateProjectDto
    {
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CompanyId { get; set; }
        public ICollection<string> LabelNames { get; set; } = new List<string>();
        public ICollection<string> AreaNames { get; set; } = new List<string>();
    }
}
