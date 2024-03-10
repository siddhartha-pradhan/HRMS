using HRMS.Domain.Constants;
using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.EmployeeJobs;

public class EmployeeJobDtoUpdate
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid InterviewId { get; set; }
    public Guid JobId { get; set; }
    public ApprovalStatus StatusApproval { get; set; }

    public static implicit operator EmployeeJob(EmployeeJobDtoUpdate employeeJobDtoUpdate)
    {
        return new EmployeeJob
        {
            Id = employeeJobDtoUpdate.Id,
            EmployeeId = employeeJobDtoUpdate.EmployeeId,
            InterviewId = employeeJobDtoUpdate.InterviewId,
            JobId = employeeJobDtoUpdate.JobId,
            Status = employeeJobDtoUpdate.StatusApproval,
            LastModifiedAt = DateTime.UtcNow
        };
    }

    public static explicit operator EmployeeJobDtoUpdate(EmployeeJob employeeJob)
    {
        return new EmployeeJobDtoUpdate
        {
            Id = employeeJob.Id,
            EmployeeId = employeeJob.EmployeeId,
            InterviewId = employeeJob.InterviewId,
            JobId = employeeJob.JobId,
            StatusApproval = employeeJob.Status
        };
    }
}
