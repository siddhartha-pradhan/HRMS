
using HRMS.Domain.Entities;

namespace API.DataTransferObjects.Companies;

public class DepartmentDtoCreate
{
    public string Title { get; set; }
    public string Description { get; set; }

    public static implicit operator Department(DepartmentDtoCreate departmentDtoCreate)
    {
        return new Department
        {
            Title = departmentDtoCreate.Title,
            Description = departmentDtoCreate.Description,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static explicit operator DepartmentDtoCreate(Department department)
    {
        return new DepartmentDtoCreate
        {
            Title = department.Title,
            Description = department.Description,
        };
    }
}
