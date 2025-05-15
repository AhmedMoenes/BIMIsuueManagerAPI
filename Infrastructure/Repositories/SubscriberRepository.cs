using Domain.Repositories;
using System;

namespace Infrastructure.Repositories
{
    public class SubscriberRepository : Repository<Subscriber>, ISubscriberRepository
    {
        public SubscriberRepository(DbContext context) : base(context) { }

        // Add custom methods if needed
    }
}
