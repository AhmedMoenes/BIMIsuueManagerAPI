using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Subscriber
    {
        public int SubscriberId { get; set; }
        public string SubscriberName { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Company> Companies { get; set; }
    }
}
