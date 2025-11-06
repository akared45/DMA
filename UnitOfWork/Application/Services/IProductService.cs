using UnitOfWork.Application.DTOs;

namespace UnitOfWork.Application.Services
{
    public interface IProductService
    {
        Task CreateProductAsync(CreateProductDto dto);
    }
}
