using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Repository;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Role? GetByName(string name)
    {
        return _dbContext.Set<Role>().FirstOrDefault(r => r.Name.ToLower() == name.ToLower());
    }
}
