using HRMS.Domain.Entities;
using HRMS.Domain.Constants;

namespace HRMS.Application.DTOs.EmployeeProjects;

public class EmployeeProjectDtoGet
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid ProjectId { get; set; }
    public ApprovalStatus StatusApproval { get; set; }

    public static implicit operator EmployeeProject(EmployeeProjectDtoGet employeeProjectDtoGet)
    {
        return new EmployeeProject
        {
            Id = employeeProjectDtoGet.Id,
            EmployeeId = employeeProjectDtoGet.EmployeeId,
            ProjectId = employeeProjectDtoGet.ProjectId,
            ApprovalStatus = employeeProjectDtoGet.StatusApproval
        };
    }

    public static explicit operator EmployeeProjectDtoGet(EmployeeProject employeeProject)
    {
        return new EmployeeProjectDtoGet
        {
            Id = employeeProject.Id,
            EmployeeId = employeeProject.EmployeeId,
            ProjectId = employeeProject.ProjectId,
            StatusApproval = employeeProject.ApprovalStatus
        };
    }
}
