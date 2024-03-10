using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Roles;

public class RoleDtoCreate
{
    public string Name { get; set; }
    
    public static implicit operator Role(RoleDtoCreate roleDtoCreate)
    {
        return new Role
        {
            Name = roleDtoCreate.Name,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public static explicit operator RoleDtoCreate(Role role)
    {
        return new RoleDtoCreate
        {
            Name = role.Name
        };
    }
}
