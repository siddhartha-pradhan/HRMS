using HRMS.Domain.Constants;
using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.EmployeeProjects;

public class EmployeeProjectDtoUpdate
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid ProjectId { get; set; }
    public ApprovalStatus StatusApproval { get; set; }

    public static implicit operator EmployeeProject(EmployeeProjectDtoUpdate employeeProjectDtoUpdate)
    {
        return new EmployeeProject
        {
            Id = employeeProjectDtoUpdate.Id,
            EmployeeId = employeeProjectDtoUpdate.EmployeeId,
            ProjectId = employeeProjectDtoUpdate.ProjectId,
            ApprovalStatus = employeeProjectDtoUpdate.StatusApproval,
            LastModifiedAt = DateTime.UtcNow
        };
    }

    public static explicit operator EmployeeProjectDtoUpdate(EmployeeProject employeeProject)
    {
        return new EmployeeProjectDtoUpdate
        {
            Id = employeeProject.Id,
            EmployeeId = employeeProject.EmployeeId,
            ProjectId = employeeProject.ProjectId,
            StatusApproval = employeeProject.ApprovalStatus
        };
    }
}
