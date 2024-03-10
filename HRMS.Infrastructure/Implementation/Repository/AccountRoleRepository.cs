using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Repository;

public class AccountRoleRepository : BaseRepository<AccountRole>, IAccountRoleRepository
{
    public AccountRoleRepository(ApplicationDbContext context) : base(context)
    {
        
    }
    
    public IEnumerable<AccountRole> GetAccountRolesByAccountGuid(Guid guid)
    {
        return _dbContext.Set<AccountRole>().Where(accountRole => accountRole.AccountId == guid);
    }
}
