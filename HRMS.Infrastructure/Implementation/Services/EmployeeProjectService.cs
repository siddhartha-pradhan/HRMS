using HRMS.Application.DTOs.EmployeeProjects;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Constants;

namespace HRMS.Infrastructure.Implementation.Services;

public class EmployeeProjectService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeProjectRepository _employeeProjectRepository;

    public EmployeeProjectService(IEmployeeRepository employeeRepository, IEmployeeProjectRepository employeeProjectRepository)
    {
        _employeeRepository = employeeRepository;
        _employeeProjectRepository = employeeProjectRepository;
    }

    public IEnumerable<EmployeeProjectDtoGet> Get()
    {
        var employeeProjects = _employeeProjectRepository.GetAll().ToList();
        
        return !employeeProjects.Any() ? Enumerable.Empty<EmployeeProjectDtoGet>() : employeeProjects.Cast<EmployeeProjectDtoGet>().ToList();
    }

    public EmployeeProjectDtoGet? Get(Guid guid)
    {
        var employeeProject = _employeeProjectRepository.GetByGuid(guid);
        
        if (employeeProject is null) return null!;

        return (EmployeeProjectDtoGet)employeeProject;
    }

    public EmployeeProjectDtoCreate? Create(EmployeeProjectDtoCreate employeeProjectDtoCreate)
    {
        var employeeProjectCreated = _employeeProjectRepository.Create(employeeProjectDtoCreate);
        
        if (employeeProjectCreated is null) return null!;

        return (EmployeeProjectDtoCreate)employeeProjectCreated;
    }

    public int Update(EmployeeProjectDtoUpdate employeeProjectDtoUpdate)
    {
        var employeeProject = _employeeProjectRepository.GetByGuid(employeeProjectDtoUpdate.Id);
        
        if (employeeProject is null) return -1;

        var employeeProjectUpdated = _employeeProjectRepository.Update(employeeProjectDtoUpdate);
        
        var employee = _employeeRepository.GetByGuid(employeeProjectDtoUpdate.EmployeeId);

        switch (employeeProjectUpdated)
        {
            case true when employeeProjectDtoUpdate.StatusApproval == ApprovalStatus.Accepted:
                employee.Status = ActionStatus.Working;
                _employeeRepository.Update(employee);
                break;
            
            case true when employeeProjectDtoUpdate.StatusApproval == ApprovalStatus.Rejected:
                employee.Status = ActionStatus.Idle;
                _employeeRepository.Update(employee);
                break;
        }
        
        return !employeeProjectUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var employeeProject = _employeeProjectRepository.GetByGuid(guid);
        
        if (employeeProject is null) return -1;

        var employeeProjectDeleted = _employeeProjectRepository.Delete(employeeProject);
        
        var employee = _employeeRepository.GetByGuid(employeeProject.EmployeeId);
        
        if (employeeProjectDeleted)
        {
            employee.Status = ActionStatus.Idle;
            
            _employeeRepository.Update(employee);
        }
        
        return employeeProjectDeleted ? 1 : 0;
    }
    
    public IEnumerable<EmployeeProjectDtoGet> GetByProject(Guid projectGuid)
    {
        var allProjects = _employeeProjectRepository.GetAll();
        
        var projectsByGuid = allProjects.Where(employeeProject => employeeProject.ProjectId == projectGuid);

        return projectsByGuid.Cast<EmployeeProjectDtoGet>().ToList();
    }
}
