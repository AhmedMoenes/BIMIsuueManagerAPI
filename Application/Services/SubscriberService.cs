namespace Application.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _repo;

        public SubscriberService(ISubscriberRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SubscriberDto>> GetAllAsync()
        {
            IEnumerable<Subscriber> subscribers = await _repo.GetAllAsync();
            return subscribers.Select(s => new SubscriberDto
            {
                SubscriberId = s.SubscriberId,
                SubscriberName = s.SubscriberName,
                CreatedAt = s.CreatedAt
            });
        }

        public async Task<SubscriberDto> GetByIdAsync(int id)
        {
            Subscriber s = await _repo.GetByIdAsync(id);
            return new SubscriberDto
            {
                SubscriberId = s.SubscriberId,
                SubscriberName = s.SubscriberName,
                CreatedAt = s.CreatedAt
            };
        }

        public async Task CreateAsync(CreateSubscriberDto dto)
        {
            Subscriber subscriber = new Subscriber
            {
                SubscriberName = dto.SubscriberName,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(subscriber);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateSubscriberDto dto)
        {
            Subscriber subscriber = await _repo.GetByIdAsync(id);
            subscriber.SubscriberName = dto.SubscriberName;

            _repo.UpdateAsync(subscriber);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Subscriber subscriber = await _repo.GetByIdAsync(id);
            _repo.DeleteAsync(subscriber);
            await _repo.SaveChangesAsync();
        }
    }
}
