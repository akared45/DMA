namespace HW7.Domain.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string username);
        bool ValidateCredentials(string username, string password);
    }
}
