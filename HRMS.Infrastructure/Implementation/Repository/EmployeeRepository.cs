using HRMS.Domain.Entities;
using HRMS.Domain.Constants;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Repository;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public bool IsDuplicateValue(string value)
    {
        return _dbContext.Set<Employee>().FirstOrDefault(e => e.Email.Contains(value) || e.PhoneNumber.Contains(value)) is null;
    }

    public Employee? GetByEmail(string email)
    {
        return _dbContext.Set<Employee>().FirstOrDefault(e => e.Email == email);
    }

    public string? GetLastEmployeeCode()
    {
        return _dbContext.Set<Employee>().ToList().Select(e => e.Code).LastOrDefault();
    }

    public int GetIdleEmployeeStatus()
    {
        return _dbContext.Set<Employee>().Count(a => a.Status == ActionStatus.Idle);
    }

    public int GetWorkingEmployeeStatus()
    {
        return _dbContext.Set<Employee>().Count(a => a.Status == ActionStatus.Working);
    }
}
