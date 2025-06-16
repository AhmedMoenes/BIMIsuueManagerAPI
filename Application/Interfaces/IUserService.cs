namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserOverviewDto> RegisterAsync(RegisterUserDto dto);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserOverviewDto> GetByIdAsync(string id);
        Task<IEnumerable<CompanyUserDto>> GetCompanyUsers(int companyId);
        Task<IEnumerable<UserOverviewDto>> GetUsersOverviewAsync();
        Task<bool> UpdateAsync(string id, UpdateUserDto dto);
        Task<bool> DeleteAsync(string id);
        Task<int> GetCompanyIdAsync(string userId);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
        Task LogoutAsync();
        Task<UserOverviewDto> CreateUserWithProjectsAsync(string adminUserId, CreateUserWithProjectsDto dto);
        Task<UserDto?> GetByEmailAsync(string email);
        Task<IEnumerable<UserDto>> GetByProjectIdAsync(int projectId);

    }
}
