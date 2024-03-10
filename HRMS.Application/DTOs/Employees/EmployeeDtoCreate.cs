using HRMS.Domain.Entities;
using HRMS.Domain.Constants;

namespace HRMS.Application.DTOs.Employees;

public class EmployeeDtoCreate
{
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

    public static implicit operator Employee(EmployeeDtoCreate employeeDtoCreate)
    {
        return new Employee
        {
            FirstName = employeeDtoCreate.FirstName,
            LastName = employeeDtoCreate.LastName ?? "",
            DateOfBirth = employeeDtoCreate.BirthDate,
            Gender = employeeDtoCreate.Gender,
            HiredDate = employeeDtoCreate.HiringDate,
            Email = employeeDtoCreate.Email,
            PhoneNumber = employeeDtoCreate.PhoneNumber,
            Status = employeeDtoCreate.Status,
            GradeId = employeeDtoCreate.GradeId,
            ProfileId = employeeDtoCreate.ProfileId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static explicit operator EmployeeDtoCreate(Employee employee)
    {
        return new EmployeeDtoCreate
        {
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
