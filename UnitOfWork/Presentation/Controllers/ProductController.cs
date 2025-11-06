using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Application.DTOs;
using UnitOfWork.Application.Services;

namespace UnitOfWork.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            await _productService.CreateProductAsync(dto);
            return Ok("Product created successfully with categories.");
        }
    }
}
