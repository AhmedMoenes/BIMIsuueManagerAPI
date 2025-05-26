namespace Infrastructure.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.ProjectId);
            builder.Property(x => x.ProjectName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.StartDate);
            builder.Property(x => x.EndDate);
        }
    }
}
