using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Jobs;

public class JobDtoCreate
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid DepartmentId { get; set; }

    public static implicit operator Job(JobDtoCreate jobDtoCreate)
    {
        return new Job
        {
            Name = jobDtoCreate.Name,
            Description = jobDtoCreate.Description,
            DepartmentId = jobDtoCreate.DepartmentId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static explicit operator JobDtoCreate(Job job)
    {
        return new JobDtoCreate
        {
            Name = job.Name,
            Description = job.Description,
            DepartmentId = job.DepartmentId
        };
    }
}
