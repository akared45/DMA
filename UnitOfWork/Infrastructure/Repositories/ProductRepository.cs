using UnitOfWork.Domain.Entities;
using UnitOfWork.Domain.Interfaces;
using UnitOfWork.Infrastructure.Data;

namespace UnitOfWork.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }
    }
}
