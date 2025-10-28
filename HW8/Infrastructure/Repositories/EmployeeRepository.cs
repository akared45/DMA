using HW8.Application.Interfaces;
using HW8.Domain.Entities;
using HW8.Infrastructure.Persistence;

namespace HW8.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeService
    {
        public EmployeeRepository(AppDbContext context) : base(context) { }
    }
}
