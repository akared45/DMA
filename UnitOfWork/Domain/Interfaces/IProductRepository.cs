using UnitOfWork.Domain.Entities;

namespace UnitOfWork.Domain.Interfaces
{
    public interface IProductRepository
    {
        void Add(Product product);
    }
}
