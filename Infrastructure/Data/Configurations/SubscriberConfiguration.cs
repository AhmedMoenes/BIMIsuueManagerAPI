namespace Infrastructure.Data.Configurations
{
    public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            builder.HasKey(x => x.SubscriberId);

            builder.Property(x => x.SubscriberName)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
