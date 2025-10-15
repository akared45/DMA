using CleanAuthSystem.Domain.Entities;
using CleanAuthSystem.Domain.Repositories;
using System.Text.Json;

namespace CleanAuthSystem.Infrastructure.Repositories
{
    public class FileUserRepository : IUserRepository
    {
        private readonly string _filePath;
        public FileUserRepository(string filePath)
        {
            _filePath = filePath;
        }
        public async Task<User?> FindByUsernameAsync(string username)
        {
            var users = await GetAllAsync();
            return users.Find(u =>
       u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
        public async Task<List<User>> GetAllAsync()
        {
            if (!File.Exists(_filePath)) return new List<User>();
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }
        public async Task SaveAsync(User user)
        {
            var users = await GetAllAsync();
            users.Add(user);
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
