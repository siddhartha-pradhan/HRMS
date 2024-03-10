using HRMS.Domain.Entities;
using HRMS.Application.Interfaces.Repositories.Base;

namespace HRMS.Application.Interfaces.Repositories;

public interface IPlacementRepository : IBaseRepository<Placement>
{
    public Placement GetByEmployeeGuid(Guid guid);
}
