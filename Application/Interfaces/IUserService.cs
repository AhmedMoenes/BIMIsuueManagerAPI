namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(string id);
        Task RegisterAsync(RegisterUserDto dto);
        Task UpdateAsync(string id, UpdateUserDto dto);
        Task DeleteAsync(string id);
    }
}
