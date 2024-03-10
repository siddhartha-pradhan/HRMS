using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Repository;

public class JobRepository : BaseRepository<Job>, IJobRepository
{
    public JobRepository(ApplicationDbContext context) : base(context)
    {
    }
}
