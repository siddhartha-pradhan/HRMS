using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Repository;

public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
{
    public ProfileRepository(ApplicationDbContext context) : base(context) { }
}
