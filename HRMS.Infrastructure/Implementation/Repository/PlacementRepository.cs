using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Repository;

public class PlacementRepository : BaseRepository<Placement>, IPlacementRepository
{
    public PlacementRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Placement GetByEmployeeGuid(Guid guid)
    {
        return _dbContext.Set<Placement>().FirstOrDefault(p => p.EmployeeId == guid);
    }
}
