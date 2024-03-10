using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Jobs;

public class JobDtoUpdate
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid DepartmentId { get; set; }
    
    public static implicit operator Job(JobDtoUpdate jobDtoUpdate)
    {
        return new Job
        {
            Id = jobDtoUpdate.Id,
            Name = jobDtoUpdate.Name,
            Description = jobDtoUpdate.Description,
            DepartmentId = jobDtoUpdate.DepartmentId,
            LastModifiedAt = DateTime.UtcNow
        };
    }

    public static explicit operator JobDtoUpdate(Job job)
    {
        return new JobDtoUpdate
        {
            Id = job.Id,
            Name = job.Name,
            Description = job.Description,
            DepartmentId = job.DepartmentId
        };
    }
}
