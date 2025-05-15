namespace Infrastructure.Data.Configurations
{
    public class ProjectTeamMemberConfiguration : IEntityTypeConfiguration<ProjectTeamMember>
    {
        public void Configure(EntityTypeBuilder<ProjectTeamMember> builder)
        {
            builder.HasKey(x => new { x.ProjectId, x.UserId });

            builder.HasOne(x => x.Project)
                .WithMany(p => p.TeamMembers)
                .HasForeignKey(x => x.ProjectId);

            builder.HasOne(x => x.User)
                .WithMany(u => u.ProjectMemberships)
                .HasForeignKey(x => x.UserId);
        }
    }
}
