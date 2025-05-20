namespace Infrastructure.Data
{
    public class DbContext :IdentityDbContext<User>
    {
        public DbContext(DbContextOptions<DbContext> options):base(options)
        {
        }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTeamMember> ProjectTeamMembers { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<RevitElement> RevitElements { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<IssueLabel> IssueLabels { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
        }
    }
}
