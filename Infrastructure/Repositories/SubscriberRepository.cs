namespace Infrastructure.Repositories
{
    public class SubscriberRepository : Repository<Subscriber>, ISubscriberRepository
    {
        public SubscriberRepository(AppDbContext context) : base(context) { }
    }
}
