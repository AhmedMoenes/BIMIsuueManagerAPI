namespace Infrastructure.Data.Configurations
{
    public class RevitElementConfiguration : IEntityTypeConfiguration<RevitElement>
    {
        public void Configure(EntityTypeBuilder<RevitElement> builder)
        {
            builder.HasKey(x => x.RevitElementId);

            builder.Property(x => x.ElementId).IsRequired();
            builder.Property(x => x.ElementUniqueId).HasMaxLength(200);
            builder.Property(x => x.ViewpointCameraPosition).HasMaxLength(500);
            builder.Property(x => x.SnapshotImagePath).HasMaxLength(500);

            builder.HasOne(x => x.Issue)
                .WithMany(i => i.RevitElements)
                .HasForeignKey(x => x.IssueId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
