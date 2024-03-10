using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Repository;

public class EmployeeJobRepository : BaseRepository<EmployeeJob>, IEmployeeJobRepository
{
    public EmployeeJobRepository(ApplicationDbContext context) : base(context)
    {
    }
}
