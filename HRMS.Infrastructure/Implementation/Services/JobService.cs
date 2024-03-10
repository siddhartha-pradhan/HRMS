using HRMS.Application.DTOs.Jobs;
using HRMS.Application.Interfaces.Repositories;

namespace HRMS.Infrastructure.Implementation.Services;

public class JobService
{
    private IJobRepository _jobRepository;

    public JobService(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public IEnumerable<JobDtoGet> Get()
    {
        var jobs = _jobRepository.GetAll().ToList();
        
        return !jobs.Any() ? Enumerable.Empty<JobDtoGet>() : jobs.Cast<JobDtoGet>().ToList();
    }

    public JobDtoGet? Get(Guid guid)
    {
        var job = _jobRepository.GetByGuid(guid);
        
        if (job is null) return null!;

        return (JobDtoGet)job;
    }

    public JobDtoCreate? Create(JobDtoCreate jobDtoCreate)
    {
        var jobCreated = _jobRepository.Create(jobDtoCreate);
        
        if (jobCreated is null) return null!;

        return (JobDtoCreate)jobCreated;
    }

    public int Update(JobDtoUpdate jobDtoUpdate)
    {
        var job = _jobRepository.GetByGuid(jobDtoUpdate.Id);
        
        if (job is null) return -1;

        var jobUpdated = _jobRepository.Update(jobDtoUpdate);
        
        return !jobUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var job = _jobRepository.GetByGuid(guid);
        
        if (job is null) return -1;

        var jobDeleted = _jobRepository.Delete(job);
        
        return !jobDeleted ? 0 : 1;
    }
}
