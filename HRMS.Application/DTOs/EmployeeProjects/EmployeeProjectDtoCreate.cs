using HRMS.Domain.Entities;
using HRMS.Domain.Constants;

namespace HRMS.Application.DTOs.EmployeeProjects;

public class EmployeeProjectDtoCreate
{
    public Guid EmployeeId { get; set; }
    public Guid ProjectId { get; set; }
    public ApprovalStatus StatusApproval { get; set; }

    public static implicit operator EmployeeProject(EmployeeProjectDtoCreate employeeProjectDtoCreate)
    {
        return new EmployeeProject
        {
            EmployeeId = employeeProjectDtoCreate.EmployeeId,
            ProjectId = employeeProjectDtoCreate.ProjectId,
            ApprovalStatus = employeeProjectDtoCreate.StatusApproval,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static explicit operator EmployeeProjectDtoCreate(EmployeeProject employeeProject)
    {
        return new EmployeeProjectDtoCreate
        {
            EmployeeId = employeeProject.EmployeeId,
            ProjectId = employeeProject.ProjectId,
            StatusApproval = employeeProject.ApprovalStatus
        };
    }
}
