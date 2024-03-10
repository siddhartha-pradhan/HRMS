using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Repository;

public class InterviewRepository : BaseRepository<Interview>, IInterviewRepository
{
    public InterviewRepository(ApplicationDbContext context) : base(context)
    {
    }
}
