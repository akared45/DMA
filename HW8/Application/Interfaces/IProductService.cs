using HW8.Domain.Entities;
using HW8.Domain.Interfaces;

namespace HW8.Application.Interfaces
{
    public interface IProductService : IRepository<Product>
    {
    }
}
