using HRMS.Domain.Entities;
using HRMS.Application.Interfaces.Repositories.Base;

namespace HRMS.Application.Interfaces.Repositories;

public interface IAccountRoleRepository : IBaseRepository<AccountRole>
{
    IEnumerable<AccountRole> GetAccountRolesByAccountGuid(Guid guid);
}
