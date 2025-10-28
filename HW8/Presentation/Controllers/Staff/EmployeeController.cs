using HW8.Domain.Entities;
using HW8.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Staff;

[ApiController]
[Route("staff/employees")]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly IRepository<Employee> _repo;

    public EmployeeController(IRepository<Employee> repo) => _repo = repo;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Employee employee)
    {
        var result = await _repo.CreateAsync(employee);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _repo.GetAllAsync();
        return Ok(result);
    }
}