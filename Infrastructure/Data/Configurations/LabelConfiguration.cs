namespace Infrastructure.Data.Configurations
{
    public class LabelConfiguration: IEntityTypeConfiguration<Label>
    {
        public void Configure(EntityTypeBuilder<Label> builder)
        {
            builder.HasKey(x => x.LabelId);
            builder.Property(p => p.LabelName).IsRequired().HasMaxLength(200);
        }
    }
}
