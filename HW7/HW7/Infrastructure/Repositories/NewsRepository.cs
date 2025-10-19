using HW7.Domain.Entities;
using HW7.Domain.Interfaces;

namespace HW7.Infrastructure.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly Dictionary<Guid, News> _newsStore = new();

        public async Task<News> GetByIdAsync(Guid id)
        {
            _newsStore.TryGetValue(id, out var news);
            return await Task.FromResult(news);
        }

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await Task.FromResult(_newsStore.Values);
        }

        public async Task AddAsync(News news)
        {
            _newsStore[news.Id] = news;
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(News news)
        {
            if (_newsStore.ContainsKey(news.Id))
            {
                _newsStore[news.Id] = news;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            _newsStore.Remove(id);
            await Task.CompletedTask;
        }
    }
}
