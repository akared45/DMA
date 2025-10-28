using HW8.Application.Interfaces;
using HW8.Domain.Entities;
using HW8.Infrastructure.Persistence;

namespace HW8.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductService
    {
        public ProductRepository(AppDbContext context) : base(context) { }
    }
}
