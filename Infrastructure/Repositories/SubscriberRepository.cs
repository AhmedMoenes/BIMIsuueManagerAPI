namespace Infrastructure.Repositories
{
    public class SubscriberRepository : Repository<Subscriber>, ISubscriberRepository
    {
        public SubscriberRepository(DbContext context) : base(context) { }
    }
}
