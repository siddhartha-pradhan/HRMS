using HRMS.Domain.Entities;
using HRMS.Domain.Constants;

namespace HRMS.Application.DTOs.Employees;

public class EmployeeDtoUpdate
{
    public Guid Id { get; set; }
    
    public string Code { get; set; }
    
    public string FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public Gender Gender { get; set; }
    
    public DateTime HiringDate { get; set; }
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public ActionStatus Status { get; set; }
    
    public Guid GradeId { get; set; }
    
    public Guid ProfileId { get; set; }
    
    public static implicit operator Employee(EmployeeDtoUpdate employeeDtoUpdate)
    {
        return new Employee
        {
            Id = employeeDtoUpdate.Id,
            Code = employeeDtoUpdate.Code,
            FirstName = employeeDtoUpdate.FirstName,
            LastName = employeeDtoUpdate.LastName ?? "",
            DateOfBirth = employeeDtoUpdate.BirthDate,
            Gender = employeeDtoUpdate.Gender,
            HiredDate = employeeDtoUpdate.HiringDate,
            Email = employeeDtoUpdate.Email,
            PhoneNumber = employeeDtoUpdate.PhoneNumber,
            Status = employeeDtoUpdate.Status,
            GradeId = employeeDtoUpdate.GradeId,
            ProfileId = employeeDtoUpdate.ProfileId,
            LastModifiedAt = DateTime.UtcNow
        };
    }

    public static explicit operator EmployeeDtoUpdate(Employee employee)
    {
        return new EmployeeDtoUpdate
        {
            Id = employee.Id,
            Code = employee.Code,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.DateOfBirth,
            Gender = employee.Gender,
            HiringDate = employee.HiredDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            Status = employee.Status,
            GradeId = employee.GradeId,
            ProfileId = employee.ProfileId
        };
    }
}
