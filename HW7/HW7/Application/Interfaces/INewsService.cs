using HW7.Application.DTOs;

namespace HW7.Application.Interfaces
{
    public interface INewsService
    {
        Task<NewsDto> GetNewsByIdAsync(Guid id);
        Task<IEnumerable<NewsDto>> GetAllNewsAsync();
        Task<NewsDto> CreateNewsAsync(CreateNewsRequest request);
        Task<NewsDto> UpdateNewsAsync(Guid id, UpdateNewsRequest request);
        Task DeleteNewsAsync(Guid id);
    }
}
