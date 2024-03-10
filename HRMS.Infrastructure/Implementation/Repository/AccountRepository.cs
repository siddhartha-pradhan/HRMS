using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Infrastructure.Implementation.Repository;

namespace API.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext context) : base(context)
    {
    }
}
