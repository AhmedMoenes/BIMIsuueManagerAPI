
namespace Infrastructure.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.CommentId);
            builder.Property(p => p.Message).IsRequired();

            builder.HasOne(x => x.Issue)
                .WithMany(i => i.Comments)
                .HasForeignKey(x => x.IssueId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
