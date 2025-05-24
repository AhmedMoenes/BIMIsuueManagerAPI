namespace Infrastructure.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.CompanyId);

            builder.Property(x => x.CompanyName)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
