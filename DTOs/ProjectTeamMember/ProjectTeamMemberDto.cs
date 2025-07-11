﻿namespace DTOs.ProjectTeamMember
{
    public class ProjectTeamMemberDto
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProjectName { get; set; }
        public string? Role { get; set; }
    }
}
