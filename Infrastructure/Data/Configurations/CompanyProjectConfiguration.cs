namespace Infrastructure.Data.Configurations
{
    public class CompanyProjectConfiguration : IEntityTypeConfiguration<CompanyProject>
    {
        public void Configure(EntityTypeBuilder<CompanyProject> builder)
        {
            builder.HasKey(cp => new { cp.CompanyId, cp.ProjectId });

            builder.HasOne(cp => cp.Company)
                .WithMany(c => c.CompanyProjects)
                .HasForeignKey(cp => cp.CompanyId);

            builder.HasOne(cp => cp.Project)
                .WithMany(p => p.CompanyProjects)
                .HasForeignKey(cp => cp.ProjectId);
        }
    }
}