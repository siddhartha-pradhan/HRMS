using HRMS.Application.DTOs.Dashboards;
using API.DataTransferObjects.Dashboards;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Constants;

namespace HRMS.Infrastructure.Implementation.Services;

public class DashboardService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeJobRepository _employeeJobRepository;
    private readonly IEmployeeProjectRepository _employeeProjectRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IPlacementRepository _placementRepository;
    private readonly IJobRepository _jobRepository;


    public DashboardService(IEmployeeRepository employeeRepository, 
        IEmployeeJobRepository employeeJobRepository, 
        IProjectRepository projectRepository, 
        IEmployeeProjectRepository employeeProjectRepository, 
        IDepartmentRepository departmentRepository, 
        IPlacementRepository placementRepository, 
        IJobRepository jobRepository)
    {
        _employeeRepository = employeeRepository;
        _employeeJobRepository = employeeJobRepository;
        _projectRepository = projectRepository;
        _employeeProjectRepository = employeeProjectRepository;
        _departmentRepository = departmentRepository;
        _placementRepository = placementRepository;
        _jobRepository = jobRepository;
    }

    public DashboardsDtoGetStatus GetEmployeeStatus()
    {
        var idleEmployeeCount = _employeeRepository.GetIdleEmployeeStatus();
        
        var workingEmployeeCount = _employeeRepository.GetWorkingEmployeeStatus();

        var status = new DashboardsDtoGetStatus
        {
            Idle = idleEmployeeCount,
            Working = workingEmployeeCount
        };

        return status;
    }

    public DashboardDtoGetInterviewStatus GetInterviewStatus()
    {
        var interviewStatus = (from j in _jobRepository.GetAll()
                               join ej in _employeeJobRepository.GetAll() 
                                   on j.Id equals ej.JobId
                               join e in _employeeRepository.GetAll() 
                                   on ej.EmployeeId equals e.Id
                               select new DashboardDtoGetInterviewStatus
                               {
                                   Accepted = ej.Status == ApprovalStatus.Accepted ? 1 : 0,
                                   Pending = ej.Status == ApprovalStatus.Pending ? 1 : 0,
                                   Rejected = ej.Status == ApprovalStatus.Rejected ? 1 : 0
                               });

        if (!interviewStatus.Any())
        {
            return null;
        }

        var status = new DashboardDtoGetInterviewStatus
        {
            Accepted = interviewStatus.Sum(i => i.Accepted),
            Pending = interviewStatus.Sum(i => i.Pending),
            Rejected = interviewStatus.Sum(i => i.Rejected)
        };

        return status;
    }

    public DashboardDtoGetInterviewStatus GetStatus()
    {
        var jobStatus = (from j in _jobRepository.GetAll()
                         join ej in _employeeJobRepository.GetAll() 
                             on j.Id equals ej.JobId
                         join e in _employeeRepository.GetAll() 
                             on ej.EmployeeId equals e.Id
                         select new DashboardDtoGetInterviewStatus
                         {
                             Accepted = ej.Status == ApprovalStatus.Accepted ? 1 : 0,
                             Pending = ej.Status == ApprovalStatus.Pending ? 1 : 0,
                             Rejected = ej.Status == ApprovalStatus.Rejected ? 1 : 0
                         });

        var projectStatus = (from p in _projectRepository.GetAll()
                             join ep in _employeeProjectRepository.GetAll() on p.Id equals ep.Id
                             join e in _employeeRepository.GetAll() on ep.EmployeeId equals e.Id
                             select new DashboardDtoGetInterviewStatus
                             {
                                 Accepted = ep.ApprovalStatus == ApprovalStatus.Accepted ? 1 : 0,
                                 Pending = ep.ApprovalStatus == ApprovalStatus.Pending ? 1 : 0,
                                 Rejected = ep.ApprovalStatus == ApprovalStatus.Rejected ? 1 : 0
                             });

        if (!jobStatus.Any() && !projectStatus.Any())
        {
            return null;
        }

        var totalAccepted = (jobStatus?.Sum(i => i.Accepted) ?? 0) + (projectStatus?.Sum(p => p.Accepted) ?? 0);
        var totalPending = (jobStatus?.Sum(i => i.Pending) ?? 0) + (projectStatus?.Sum(p => p.Pending) ?? 0);
        var totalRejected = (jobStatus?.Sum(i => i.Rejected) ?? 0) + (projectStatus?.Sum(p => p.Rejected) ?? 0);

        var status = new DashboardDtoGetInterviewStatus
        {
            Accepted = totalAccepted,
            Pending = totalPending,
            Rejected = totalRejected
        };

        return status;
    }

    public IEnumerable<DashboardDtoGetClient> GetTop5Client()
    {
        var getClient = _placementRepository.GetAll()
            .Join(_departmentRepository.GetAll(),
                placement => placement.DepartmentId,
                company => company.Id,
                (placement, department) => new DashboardDtoGetClient
                {
                    DepartmentId = placement.DepartmentId,
                    Name = department.Title
                })
            .GroupBy(placement => placement.DepartmentId)
            .Select(group => new DashboardDtoGetClient
            {
                DepartmentId = group.Key,
                Name = group.First().Name, 
                TotalEmployees = group.Count()
            })
            .OrderByDescending(x => x.TotalEmployees)
            .Take(5);

        return getClient;
    }
}
