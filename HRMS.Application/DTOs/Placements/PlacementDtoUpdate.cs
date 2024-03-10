using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Placements;

public class PlacementDtoUpdate
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid DepartmentId { get; set; }

    public static implicit operator Placement(PlacementDtoUpdate placementDtoUpdate)
    {
        return new Placement
        {
            Id = placementDtoUpdate.Id,
            Title = placementDtoUpdate.Title,
            Description = placementDtoUpdate.Description,
            EmployeeId = placementDtoUpdate.EmployeeId,
            DepartmentId = placementDtoUpdate.DepartmentId,
            LastModifiedAt = DateTime.UtcNow
        };
    }

    public static explicit operator PlacementDtoUpdate(Placement placement)
    {
        return new PlacementDtoUpdate
        {
            Id = placement.Id,
            Title = placement.Title,
            Description = placement.Description,
            EmployeeId = placement.EmployeeId,
            DepartmentId = placement.DepartmentId
        };
    }
}
