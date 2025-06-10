namespace Infrastructure.Data.Configurations
{
    public class RevitElementConfiguration : IEntityTypeConfiguration<RevitElement>
    {
        public void Configure(EntityTypeBuilder<RevitElement> builder)
        {
            builder.HasKey(x => x.RevitElementId);

            builder.Property(x => x.ElementId).IsRequired();
            builder.Property(x => x.ElementUniqueId).IsRequired();
            builder.Property(x => x.ViewpointCameraPosition).IsRequired();
            builder.Property(x => x.ViewpointForwardDirection).IsRequired();
            builder.Property(x => x.ViewpointUpDirection).IsRequired();

            builder.HasOne(x => x.Issue)
                   .WithMany(i => i.RevitElements)
                   .HasForeignKey(x => x.IssueId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
