namespace Infrastructure.Data.Configurations
{
    public class ProjectTeamMemberConfiguration : IEntityTypeConfiguration<ProjectTeamMember>
    {
        public void Configure(EntityTypeBuilder<ProjectTeamMember> builder)
        {
            builder.HasKey(x => new { x.ProjectId, x.UserId });

            builder.Property(x => x.Role)
                   .IsRequired();

            builder.HasOne(x => x.Project)
                .WithMany(p => p.ProjectTeamMembers)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                   .WithMany(u => u.ProjectMemberships)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
