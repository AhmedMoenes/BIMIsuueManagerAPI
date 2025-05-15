namespace Infrastructure.Data.Configurations
{
    public class IssueLabelConfiguration : IEntityTypeConfiguration<IssueLabel>
    {
        public void Configure(EntityTypeBuilder<IssueLabel> builder)
        {
            builder.HasKey(x => new { x.IssueId, x.LabelId });

            builder.HasOne(x => x.Issue)
                .WithMany(i => i.Labels)
                .HasForeignKey(x => x.IssueId);

            builder.HasOne(x => x.Label)
                .WithMany(l => l.Issues)
                .HasForeignKey(x => x.LabelId);
        }
    }
}
