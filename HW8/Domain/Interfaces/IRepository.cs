using HW8.Domain.Entities;

namespace HW8.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
    }
}
