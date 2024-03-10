using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Roles;

public class RoleDtoGet
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public static implicit operator Role(RoleDtoGet roleDtoGet)
    {
        return new Role
        {
            Id = roleDtoGet.Id,
            Name = roleDtoGet.Name
        };
    }
    
    public static explicit operator RoleDtoGet(Role role)
    {
        return new RoleDtoGet
        {
            Id = role.Id,
            Name = role.Name
        };
    }
}
