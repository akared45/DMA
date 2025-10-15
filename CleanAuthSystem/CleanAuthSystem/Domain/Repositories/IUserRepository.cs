using CleanAuthSystem.Domain.Entities;

namespace CleanAuthSystem.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> FindByUsernameAsync(string username);
        Task SaveAsync(User user);
        Task<List<User>> GetAllAsync();
    }
}
