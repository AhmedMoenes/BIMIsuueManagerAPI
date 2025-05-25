using DTOs.Areas;
using DTOs.Labels;
using DTOs.ProjectTeamMember;

namespace DTOs.Projects
{
    public class CreateProjectDto
    {
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CompanyId { get; set; }
        public ICollection<ProjectTeamMemberDto> TeamMembers { get; set; }
        public ICollection<LabelDto> Labels { get; set; }
        public ICollection<AreaDto> Areas { get; set; }
    }
}
