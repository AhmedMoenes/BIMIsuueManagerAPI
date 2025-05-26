using DTOs.Areas;
using DTOs.Labels;

namespace DTOs.Projects
{
    public class CreateProjectDto
    {
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<CreateLabelDto> Labels { get; set; }
        public ICollection<CreateAreaDto> Areas { get; set; }
    }
}
