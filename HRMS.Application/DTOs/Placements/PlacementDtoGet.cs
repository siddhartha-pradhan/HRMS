using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Placements;

public class PlacementDtoGet
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid DepartmentId { get; set; }

    public static implicit operator Placement(PlacementDtoGet placementDtoGet)
    {
        return new Placement
        {
            Id = placementDtoGet.Id,
            Title = placementDtoGet.Title,
            Description = placementDtoGet.Description,
            EmployeeId = placementDtoGet.EmployeeId,
            DepartmentId = placementDtoGet.DepartmentId
        };
    }

    public static explicit operator PlacementDtoGet(Placement placement)
    {
        return new PlacementDtoGet
        {
            Id = placement.Id,
            Title = placement.Title,
            Description = placement.Description,
            EmployeeId = placement.EmployeeId,
            DepartmentId = placement.DepartmentId
        };
    }
}
