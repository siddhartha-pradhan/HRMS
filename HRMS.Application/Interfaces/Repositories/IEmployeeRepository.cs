using HRMS.Domain.Entities;
using HRMS.Application.Interfaces.Repositories.Base;

namespace HRMS.Application.Interfaces.Repositories;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    bool IsDuplicateValue(string value);
    
    Employee? GetByEmail(string email);
    
    string? GetLastEmployeeCode();
    
    int GetIdleEmployeeStatus();
    
    int GetWorkingEmployeeStatus();
}
