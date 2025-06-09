namespace Infrastructure.Data.Configurations
{
    public class SnapshotConfiguration : IEntityTypeConfiguration<Snapshot>
    {
        public void Configure(EntityTypeBuilder<Snapshot> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Path)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.HasOne(x => x.Issue)
                .WithMany(i => i.Snapshots)
                .HasForeignKey(x => x.IssueId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Comments)
                .WithOne(c => c.Snapshot)
                .HasForeignKey(c => c.SnapshotId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
