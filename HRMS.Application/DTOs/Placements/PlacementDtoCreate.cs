using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Placements;

public class PlacementDtoCreate
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid DepartmentId { get; set; }

    public static implicit operator Placement(PlacementDtoCreate placementDtoCreate)
    {
        return new Placement
        {
            Title = placementDtoCreate.Title,
            Description = placementDtoCreate.Description,
            EmployeeId = placementDtoCreate.EmployeeId,
            DepartmentId = placementDtoCreate.DepartmentId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static explicit operator PlacementDtoCreate(Placement placement)
    {
        return new PlacementDtoCreate
        {
            Title = placement.Title,
            Description = placement.Description,
            EmployeeId = placement.EmployeeId,
            DepartmentId = placement.DepartmentId
        };
    }
}
