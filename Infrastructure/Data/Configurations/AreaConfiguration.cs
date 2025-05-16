
namespace Infrastructure.Data.Configurations
{
    public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.HasKey(x => x.AreaId);
            builder.Property(p => p.AreaName);
        }
    }
}
