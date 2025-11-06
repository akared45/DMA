using UnitOfWork.Domain.Entities;

namespace UnitOfWork.Domain.Interfaces
{
    public interface IProductCategoryRepository
    {
        void Add(ProductCategory productCategory);
    }
}
