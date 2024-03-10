using API.DataTransferObjects.Companies;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Services;

public class DepartmentService
{
    private IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public IEnumerable<DepartmentDtoGet> Get()
    {
        var companies = _departmentRepository.GetAll().ToList();
        
        return !companies.Any() ? Enumerable.Empty<DepartmentDtoGet>() : companies.Cast<DepartmentDtoGet>().ToList();
    }

    public DepartmentDtoGet? Get(Guid guid)
    {
        var department = _departmentRepository.GetByGuid(guid);
        
        if (department is null) return null!;

        return (DepartmentDtoGet)department;
    }

    public DepartmentDtoCreate? Create(DepartmentDtoCreate departmentDtoCreate)
    {
        var departmentCreated = _departmentRepository.Create(departmentDtoCreate);
        
        if (departmentCreated is null) return null!;

        return (DepartmentDtoCreate)departmentCreated;
    }

    public int Update(DepartmentDtoUpdate departmentDtoUpdate)
    {
        var department = _departmentRepository.GetByGuid(departmentDtoUpdate.Id);
        
        if (department is null) return -1;

        var departmentUpdated = _departmentRepository.Update(departmentDtoUpdate);
        
        return !departmentUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var department = _departmentRepository.GetByGuid(guid);
        
        if (department is null) return -1;

        var departmentDeleted = _departmentRepository.Delete(department);
        
        return !departmentDeleted ? 0 : 1;
    }
}
