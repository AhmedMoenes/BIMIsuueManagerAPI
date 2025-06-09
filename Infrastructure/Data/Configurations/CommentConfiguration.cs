namespace Infrastructure.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.CommentId);

            builder.Property(p => p.Message)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.HasOne(x => x.Issue)
                .WithMany(i => i.Comments)
                .HasForeignKey(x => x.IssueId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Snapshot)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.SnapshotId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.CreatedByUser)
                .WithMany(u => u.CommentsCreated)
                .HasForeignKey(f => f.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
