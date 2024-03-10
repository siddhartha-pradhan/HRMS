using HRMS.Domain.Constants;
using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.EmployeeJobs;

public class EmployeeJobDtoCreate
{
    public Guid EmployeeId { get; set; }
    public Guid InterviewId { get; set; }
    public Guid JobId { get; set; }
    public ApprovalStatus StatusApproval { get; set; }

    public static implicit operator EmployeeJob(EmployeeJobDtoCreate employeeJobDtoCreate)
    {
        return new EmployeeJob
        {
            EmployeeId = employeeJobDtoCreate.EmployeeId,
            InterviewId = employeeJobDtoCreate.InterviewId,
            JobId = employeeJobDtoCreate.JobId,
            Status = employeeJobDtoCreate.StatusApproval,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static explicit operator EmployeeJobDtoCreate(EmployeeJob employeeJob)
    {
        return new EmployeeJobDtoCreate
        {
            EmployeeId = employeeJob.EmployeeId,
            InterviewId = employeeJob.InterviewId,
            JobId = employeeJob.JobId,
            StatusApproval = employeeJob.Status
        };
    }
}
