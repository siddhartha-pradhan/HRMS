
using HRMS.Domain.Entities;

namespace API.DataTransferObjects.Companies;

public class DepartmentDtoGet
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public static implicit operator Department(DepartmentDtoGet departmentDtoGet)
    {
        return new Department
        {
            Id = departmentDtoGet.Id,
            Title = departmentDtoGet.Title,
            Description = departmentDtoGet.Description
        };
    }

    public static explicit operator DepartmentDtoGet(Department department)
    {
        return new DepartmentDtoGet
        {
            Id = department.Id,
            Title = department.Title,
            Description = department.Description
        };
    }
}
