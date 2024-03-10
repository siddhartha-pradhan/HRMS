using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Jobs;

public class JobDtoGet
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid DepartmentId { get; set; }

    public static implicit operator Job(JobDtoGet jobDtoGet)
    {
        return new Job
        {
            Id = jobDtoGet.Id,
            Name = jobDtoGet.Name,
            Description = jobDtoGet.Description,
            DepartmentId = jobDtoGet.DepartmentId
        };
    }

    public static explicit operator JobDtoGet(Job job)
    {
        return new JobDtoGet
        {
            Id = job.Id,
            Name = job.Name,
            Description = job.Description,
            DepartmentId = job.DepartmentId
        };
    }
}
