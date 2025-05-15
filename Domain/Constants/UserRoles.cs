namespace Domain.Constants
{
    public static class UserRoles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string ProjectLeader = "ProjectLeader";
        public const string Editor = "Editor";
        public const string Reviewer = "Reviewer";
        public const string Viewer = "Viewer";

        public static readonly List<string> RolesList = new List<string>()
        {
            SuperAdmin,
            Admin,
            ProjectLeader,
            Editor,
            Reviewer,
            Viewer
        };
    }
}
