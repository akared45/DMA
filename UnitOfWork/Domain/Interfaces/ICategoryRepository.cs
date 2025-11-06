using UnitOfWork.Domain.Entities;

namespace UnitOfWork.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        void Add(Category category);
    }
}
