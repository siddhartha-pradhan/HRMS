﻿using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Repository;

public class EmployeeProjectRepository : BaseRepository<EmployeeProject>, IEmployeeProjectRepository
{
    public EmployeeProjectRepository(ApplicationDbContext Context) : base(Context) { }
}
