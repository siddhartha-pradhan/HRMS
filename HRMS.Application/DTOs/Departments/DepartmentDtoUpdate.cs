using HRMS.Domain.Entities;

namespace API.DataTransferObjects.Companies;

public class DepartmentDtoUpdate
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public static implicit operator Department(DepartmentDtoUpdate departmentDtoUpdate)
    {
        return new Department
        {
            Id = departmentDtoUpdate.Id,
            Title = departmentDtoUpdate.Title,
            Description = departmentDtoUpdate.Description,
            LastModifiedAt = DateTime.UtcNow
        };
    }

    public static explicit operator DepartmentDtoUpdate(Department department)
    {
        return new DepartmentDtoUpdate
        {
            Id = department.Id,
            Title = department.Title,
            Description = department.Description,
        };
    }
}
