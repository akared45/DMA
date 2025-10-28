using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HW8.Application.DTOs.MenuDto;

namespace HW8.Presentation.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class MenuController : ControllerBase
    {
        [HttpGet("menu")]
        public IActionResult GetMenu()
        {
            var menu = new MenuResponse
            {
                Items = new List<MenuItemDto>
            {
                new MenuItemDto
                {
                    Name = "Admin",
                    SubItems = new List<MenuItemDto>
                    {
                        new() { Name = "Create Article" },
                        new() { Name = "Show Article" }
                    }
                },
                new MenuItemDto
                {
                    Name = "Manager",
                    SubItems = new List<MenuItemDto>
                    {
                        new() { Name = "Create Product" },
                        new() { Name = "Show Product" }
                    }
                },
                new MenuItemDto
                {
                    Name = "Staff",
                    SubItems = new List<MenuItemDto>
                    {
                        new() { Name = "Create Employee" },
                        new() { Name = "Show Employee" }
                    }
                }
            }
            };

            return Ok(menu);
        }
    }
}
