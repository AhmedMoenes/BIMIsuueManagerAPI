﻿using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Position { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<ProjectTeamMember> ProjectMemberships { get; set; }
        public ICollection<Issue> CreatedIssues { get; set; }
        public ICollection<Issue> AssignedIssues { get; set; }
        public ICollection<Comment> CommentsCreated { get; set; }
    }
}
