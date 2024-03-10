using HRMS.Application.Interfaces.Repositories.Base;
using HRMS.Domain.Entities;

namespace HRMS.Application.Interfaces.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Role? GetByName(string name);
}
