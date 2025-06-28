using DTOs.Issues;
using DTOs.ProjectTeamMember;

namespace DTOs.Users
{
    public class CurrentUserDto
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CompanyId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
        public string CompanyName { get; set; }
        public List<ProjectTeamMemberDto> ProjectMemberships { get; set; } = new();
        public List<IssueDto> CreatedIssues { get; set; } = new();
        public List<IssueDto> AssignedIssues { get; set; } = new();
        public int ProjectsIncludedCount { get; set; }
        public int IssuesCreatedCount { get; set; }
        public int IssuesAssignedCount { get; set; }
    }
}
