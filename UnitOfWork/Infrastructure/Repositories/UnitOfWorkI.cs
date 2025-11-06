using UnitOfWork.Domain.Interfaces;
using UnitOfWork.Infrastructure.Data;

namespace UnitOfWork.Infrastructure.Repositories
{
    public class UnitOfWorkI : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public IProductCategoryRepository ProductCategories { get; }

        public UnitOfWorkI(AppDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
            ProductCategories = new ProductCategoryRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
