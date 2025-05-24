namespace Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user, string role);
    }
}
