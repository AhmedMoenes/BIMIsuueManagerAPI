using DTOs.Issues;
using DTOs.ProjectTeamMember;

namespace DTOs.Login
{
    public class LoginResponseDto
    {
        public string Token { get; set; } 
        public string Role { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CompanyName { get; set; }
        public List<ProjectTeamMemberDto> ProjectMemberships { get; set; } = new();
        public List<IssueDto> CreatedIssues { get; set; } = new();
        public List<IssueDto> AssignedIssues { get; set; } = new();
    }
}
