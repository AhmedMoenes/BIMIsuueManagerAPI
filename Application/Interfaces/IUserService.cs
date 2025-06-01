namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(RegisterUserDto dto);     
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserOverviewDto> GetByIdAsync(string id);
        Task<IEnumerable<UserOverviewDto>> GetUsersOverviewAsync();
        Task<bool> UpdateAsync(string id, UpdateUserDto dto); 
        Task<bool> DeleteAsync(string id);
        Task<int> GetCompanyIdAsync(string userId);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
        Task<UserDto> CreateUserWithProjectsAsync(string adminUserId, CreateUserWithProjectsDto dto);
        Task<UserDto?> GetByEmailAsync(string email);
        Task<IEnumerable<UserDto>> GetByProjectIdAsync(int projectId);

    }
}
