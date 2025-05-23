using Application.DTOs.Users;

namespace Application.Interfaces
{
    public interface ISubscriberService
    {
        Task<IEnumerable<SubscriberDto>> GetAllAsync();
        Task<SubscriberDto> GetByIdAsync(int id);
        Task<SubscriberDto> CreateAsync(CreateSubscriberDto dto);
        Task UpdateAsync(int id, UpdateSubscriberDto dto);
        Task DeleteAsync(int id);
    }
}
