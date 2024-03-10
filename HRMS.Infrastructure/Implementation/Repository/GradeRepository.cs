using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Repository;

public class GradeRepository : BaseRepository<Grade>, IGradeRepository
{
    public GradeRepository(ApplicationDbContext Context) : base(Context) { }
}
