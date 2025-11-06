using UnitOfWork.Application.DTOs;
using UnitOfWork.Domain.Entities;
using UnitOfWork.Domain.Interfaces;

namespace UnitOfWork.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateProductAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                Price = dto.Price
            };

            _unitOfWork.Products.Add(product);

            foreach (var categoryName in dto.CategoryNames)
            {
                var category = new Category { CategoryName = categoryName };
                _unitOfWork.Categories.Add(category);

                var productCategory = new ProductCategory
                {
                    Product = product,
                    Category = category
                };
                _unitOfWork.ProductCategories.Add(productCategory);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}