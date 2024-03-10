using HRMS.Application.DTOs.Roles;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Services;

public class RoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public IEnumerable<RoleDtoGet> Get()
    {
        var roles = _roleRepository.GetAll().ToList();
        
        return !roles.Any() ? Enumerable.Empty<RoleDtoGet>() : roles.Cast<RoleDtoGet>().ToList();
    }

    public RoleDtoGet? Get(Guid guid)
    {
        var role = _roleRepository.GetByGuid(guid);
        
        if (role is null) return null!;

        return (RoleDtoGet)role;
    }

    public RoleDtoCreate? Create(RoleDtoCreate roleDtoCreate)
    {
        var roleCreated = _roleRepository.Create(roleDtoCreate);
        
        if (roleCreated is null) return null!;

        return (RoleDtoCreate)roleCreated;
    }

    public int Update(RoleDtoUpdate roleDtoUpdate)
    {
        var role = _roleRepository.GetByGuid(roleDtoUpdate.Id);
        
        if (role is null) return -1;

        var roleUpdated = _roleRepository.Update(roleDtoUpdate);
        
        return !roleUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var role = _roleRepository.GetByGuid(guid);
        
        if (role is null) return -1;

        var roleDeleted = _roleRepository.Delete(role);
        
        return !roleDeleted ? 0 : 1;
    }
}
