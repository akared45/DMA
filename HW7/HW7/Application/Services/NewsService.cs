using HW7.Application.DTOs;
using HW7.Application.Interfaces;
using HW7.Domain.Entities;
using HW7.Domain.Interfaces;
using Prometheus;
using System.Diagnostics.Metrics;

namespace HW7.Application.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly ILogger<NewsService> _logger;

        private static readonly Counter CreateCounter = Metrics.CreateCounter("news_created_total", "Total news created");
        private static readonly Counter UpdateCounter = Metrics.CreateCounter("news_updated_total", "Total news updated");
        private static readonly Counter DeleteCounter = Metrics.CreateCounter("news_deleted_total", "Total news deleted");
        private static readonly Counter ReadCounter = Metrics.CreateCounter("news_read_total", "Total news read");

        public NewsService(INewsRepository newsRepository, ILogger<NewsService> logger)
        {
            _newsRepository = newsRepository;
            _logger = logger;
        }

        public async Task<NewsDto> GetNewsByIdAsync(Guid id)
        {

            var news = await _newsRepository.GetByIdAsync(id);
            if (news == null)
            {
                _logger.LogWarning("News with Id {NewsId} not found", id);
                return null;
            }

            _logger.LogInformation("Retrieved news with Id {NewsId}", id);
            ReadCounter.Inc();
            return MapToDto(news);
        }

        public async Task<IEnumerable<NewsDto>> GetAllNewsAsync()
        {
            var newsList = await _newsRepository.GetAllAsync();
            _logger.LogInformation("Retrieved {Count} news items", newsList.Count());
            ReadCounter.Inc(newsList.Count());
            return newsList.Select(MapToDto);
        }

        public async Task<NewsDto> CreateNewsAsync(CreateNewsRequest request)
        {
            var news = new News
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content
            };

            await _newsRepository.AddAsync(news);
            _logger.LogInformation("Created news with Id {NewsId} and Title {Title}", news.Id, news.Title);
            CreateCounter.Inc();
            return MapToDto(news);
        }

        public async Task<NewsDto> UpdateNewsAsync(Guid id, UpdateNewsRequest request)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news == null)
            {
                _logger.LogWarning("News with Id {NewsId} not found for update", id);
                return null;
            }

            news.Title = request.Title;
            news.Content = request.Content;

            await _newsRepository.UpdateAsync(news);
            _logger.LogInformation("Updated news with Id {NewsId} to Title {Title}", id, request.Title);
            UpdateCounter.Inc();

            return MapToDto(news);
        }

        public async Task DeleteNewsAsync(Guid id)
        {
            var news = await _newsRepository.GetByIdAsync(id);
            if (news == null)
            {
                _logger.LogWarning("News with Id {NewsId} not found for delete", id);
                return;
            }

            await _newsRepository.DeleteAsync(id);
            _logger.LogInformation("Deleted news with Id {NewsId}", id);
            DeleteCounter.Inc();
        }
        private NewsDto MapToDto(News news)
        {
            return new NewsDto
            {
                Id = news.Id,
                Title = news.Title,
                Content = news.Content,
                CreatedAt = news.CreatedAt
            };
        }
    }
}

