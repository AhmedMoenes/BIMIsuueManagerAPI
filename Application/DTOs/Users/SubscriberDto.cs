namespace Application.DTOs.Users
{
    public class SubscriberDto
    {
        public int SubscriberId { get; set; }
        public string SubscriberName { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Company> Companies { get; set; }
    }
}
