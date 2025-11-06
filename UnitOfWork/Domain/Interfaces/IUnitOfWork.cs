namespace UnitOfWork.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IProductCategoryRepository ProductCategories { get; }
        Task<int> SaveChangesAsync();
    }
}
