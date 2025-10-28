using HW8.Domain.Entities;
using HW8.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HW8.Presentation.Controllers.Admin
{
    [ApiController]
    [Route("admin/articles")]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private readonly IRepository<Article> _repo;

        public ArticleController(IRepository<Article> repo) => _repo = repo;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Article article)
        {
            var result = await _repo.CreateAsync(article);
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
