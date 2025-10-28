using HW8.Domain.Entities;
using HW8.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HW8.Presentation.Controllers.Manager
{
    [ApiController]
    [Route("manager/products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _repo;

        public ProductController(IRepository<Product> repo) => _repo = repo;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var result = await _repo.CreateAsync(product);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repo.GetAllAsync();
            return Ok(result);
        }
    }
}
