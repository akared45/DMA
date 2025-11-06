using UnitOfWork.Domain.Entities;
using UnitOfWork.Domain.Interfaces;
using UnitOfWork.Infrastructure.Data;

namespace UnitOfWork.Infrastructure.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly AppDbContext _context;

        public ProductCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
        }
    }
}
