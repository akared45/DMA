using UnitOfWork.Domain.Entities;
using UnitOfWork.Domain.Interfaces;
using UnitOfWork.Infrastructure.Data;

namespace UnitOfWork.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }
    }
}
