using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            // Issue
            builder.HasKey(x => x.IssueId);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(1000); 

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.Priority)
                .IsRequired();

            // Booleans
            builder.Property(x => x.IsResolved)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Project
            builder.HasOne(x => x.Project)
                .WithMany(p => p.Issues)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Area
            builder.HasOne(x => x.Area)
                .WithMany(a => a.Issues)
                .HasForeignKey(x => x.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Created By
            builder.HasOne(x => x.CreatedByUser)
                .WithMany(u => u.CreatedIssues)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Assigned To
            builder.HasOne(x => x.AssignedToUser)
                .WithMany(u => u.AssignedIssues)
                .HasForeignKey(x => x.AssignedToUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
