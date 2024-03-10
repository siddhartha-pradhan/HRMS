using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Roles;

public class RoleDtoUpdate
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public static implicit operator Role(RoleDtoUpdate roleDtoUpdate)
    {
        return new Role
        {
            Id = roleDtoUpdate.Id,
            Name = roleDtoUpdate.Name,
            LastModifiedAt = DateTime.UtcNow
        };
    }

    public static explicit operator RoleDtoUpdate(Role role)
    {
        return new RoleDtoUpdate
        {
            Id = role.Id,
            Name = role.Name
        };
    }
}
