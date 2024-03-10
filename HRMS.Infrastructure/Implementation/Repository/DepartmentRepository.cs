using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Repository;

public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
