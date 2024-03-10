using HRMS.Domain.Constants;
using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.EmployeeJobs;

public class EmployeeJobDtoGet
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid InterviewId { get; set; }
    public Guid JobId { get; set; }
    public ApprovalStatus StatusApproval { get; set; }

    public static implicit operator EmployeeJob(EmployeeJobDtoGet employeeJobDtoGet)
    {
        return new EmployeeJob
        {
            Id = employeeJobDtoGet.Id,
            EmployeeId = employeeJobDtoGet.EmployeeId,
            InterviewId = employeeJobDtoGet.InterviewId,
            JobId = employeeJobDtoGet.JobId,
            Status = employeeJobDtoGet.StatusApproval
        };
    }

    public static explicit operator EmployeeJobDtoGet(EmployeeJob employeeJob)
    {
        return new EmployeeJobDtoGet
        {
            Id = employeeJob.Id,
            EmployeeId = employeeJob.EmployeeId,
            InterviewId = employeeJob.InterviewId,
            JobId = employeeJob.JobId,
            StatusApproval = employeeJob.Status
        };
    }
}
