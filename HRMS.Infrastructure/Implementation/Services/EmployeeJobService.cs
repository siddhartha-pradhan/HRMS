using HRMS.Application.DTOs.Interviews;
using HRMS.Application.DTOs.Placements;
using HRMS.Application.DTOs.EmployeeJobs;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Constants;

namespace HRMS.Infrastructure.Implementation.Services;

public class EmployeeJobService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IEmployeeJobRepository _employeeJobRepository;
    private readonly IPlacementRepository _placementRepository;
    private readonly IInterviewRepository _interviewRepository;
    private readonly IJobRepository _jobRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeJobService(IDepartmentRepository departmentRepository, 
        IEmployeeJobRepository employeeJobRepository, 
        IPlacementRepository placementRepository,
        IInterviewRepository interviewRepository, 
        IJobRepository jobRepository, 
        IEmployeeRepository employeeRepository)
    {
        _departmentRepository = departmentRepository;
        _employeeJobRepository = employeeJobRepository;
        _placementRepository = placementRepository;
        _interviewRepository = interviewRepository;
        _jobRepository = jobRepository;
        _employeeRepository = employeeRepository;
    }

    public IEnumerable<EmployeeJobDtoGet> Get()
    {
        var employeeJobs = _employeeJobRepository.GetAll().ToList();
        
        return !employeeJobs.Any() ? Enumerable.Empty<EmployeeJobDtoGet>() : employeeJobs.Cast<EmployeeJobDtoGet>().ToList();
    }

    public EmployeeJobDtoGet? Get(Guid guid)
    {
        var employeeJob = _employeeJobRepository.GetByGuid(guid);
        
        if (employeeJob is null) return null!;

        return (EmployeeJobDtoGet)employeeJob;
    }

    public EmployeeJobDtoCreate? Create(EmployeeJobDtoCreate employeeJobDtoCreate)
    {
        var job = _jobRepository.GetByGuid(employeeJobDtoCreate.JobId);
        
        var department = _departmentRepository.GetByGuid(job!.DepartmentId);
        
        var employee = _employeeRepository.GetByGuid(employeeJobDtoCreate.EmployeeId);

        var interviewDtoCreate = new InterviewDtoCreate
        {
            Title = $"{job.Name} at {department?.Title} - {employee?.Code} {employee?.FirstName} {employee?.LastName}",
            Link = "",
            InterviewDate = DateTime.Now,
            Description = ""
        };

        var interviewCreated = _interviewRepository.Create(interviewDtoCreate);
        
        employeeJobDtoCreate.InterviewId = interviewCreated.Id;
        
        var employeeJobCreated = _employeeJobRepository.Create(employeeJobDtoCreate);

        if (employeeJobCreated is null) return null!;

        return (EmployeeJobDtoCreate)employeeJobCreated;
    }

    public int Update(EmployeeJobDtoUpdate employeeJobDtoUpdate)
    {
        var employeeJob = _employeeJobRepository.GetByGuid(employeeJobDtoUpdate.Id);
        
        if (employeeJob is null) return -1;

        var employeeJobUpdated = _employeeJobRepository.Update(employeeJobDtoUpdate);
        
        var employee = _employeeRepository.GetByGuid(employeeJobDtoUpdate.EmployeeId);
        
        var job = _jobRepository.GetByGuid(employeeJobDtoUpdate.JobId);
        
        var department = _departmentRepository.GetByGuid(job!.DepartmentId);
        
        if (employeeJobUpdated)
        {
            if (employeeJobDtoUpdate.StatusApproval == ApprovalStatus.Accepted)
            {
                employee!.Status = ActionStatus.Working;
                
                _employeeRepository.Update(employee);
                
                var placementDtoCreate = new PlacementDtoCreate
                {
                    Title = $"{job.Name} at {department!.Title}",
                    Description = job.Description,
                    EmployeeId = employeeJobDtoUpdate.EmployeeId,
                    DepartmentId = department.Id
                };
                
                var placementCreated = _placementRepository.Create(placementDtoCreate);
            }

            if (employeeJobDtoUpdate.StatusApproval == ApprovalStatus.Rejected)
            {
                employee!.Status = ActionStatus.Idle;
                
                _employeeRepository.Update(employee);
                
                var placement = _placementRepository.GetByEmployeeGuid(employeeJobDtoUpdate.EmployeeId);

                if (placement != null)
                {
                    if (placement.EmployeeId == employeeJobDtoUpdate.EmployeeId && placement.DepartmentId == department!.Id) 
                        _placementRepository.Delete(placement);
                }
            }
        }
        return employeeJobUpdated ? 1 : 0;
    }

    public int Delete(Guid guid)
    {
        var employeeJob = _employeeJobRepository.GetByGuid(guid);
        
        if (employeeJob is null) return -1;
        
        var employee = _employeeRepository.GetByGuid(employeeJob.EmployeeId);

        var employeeJobDeleted = _employeeJobRepository.Delete(employeeJob);

        var placement = _placementRepository.GetByEmployeeGuid(employeeJob.EmployeeId);
        
        var placementDtoGets = placement;

        var job = _jobRepository.GetByGuid(employeeJob.JobId);
        
        var department = _departmentRepository.GetByGuid(job!.DepartmentId);
        
        var interview = _interviewRepository.GetByGuid(employeeJob.InterviewId);

        if (employeeJobDeleted)
        {
            employee.Status = ActionStatus.Idle;
            
            _employeeRepository.Update(employee);
            
            _interviewRepository.Delete(interview);
            
            if (placementDtoGets != null)
            {
                if (placementDtoGets.EmployeeId == employeeJob.EmployeeId && placementDtoGets.DepartmentId == department.Id)
                    _placementRepository.Delete(placementDtoGets);
            }
        }
        return !employeeJobDeleted ? 0 : 1;
    }

    public IEnumerable<EmployeeJobDtoGet> GetByJob(Guid jobGuid)
    {
        var allJobs = _employeeJobRepository.GetAll();
        var employeeJobs = allJobs.Where(employeeJob => employeeJob.JobId == jobGuid);

        List<EmployeeJobDtoGet> employeeJobDtoGets = new();

        foreach (var employeeJob in employeeJobs)
        {
            employeeJobDtoGets.Add((EmployeeJobDtoGet)employeeJob);
        }

        return employeeJobDtoGets;
    }
}
