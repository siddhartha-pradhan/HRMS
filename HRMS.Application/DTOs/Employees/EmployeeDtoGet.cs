using HRMS.Domain.Entities;
using HRMS.Domain.Constants;

namespace HRMS.Application.DTOs.Employees;

public class EmployeeDtoGet
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

    public static implicit operator Employee(EmployeeDtoGet employeeDtoGet)
    {
        return new Employee
        {
            Id = employeeDtoGet.Id,
            Code = employeeDtoGet.Code,
            FirstName = employeeDtoGet.FirstName,
            LastName = employeeDtoGet.LastName ?? "",
            DateOfBirth = employeeDtoGet.BirthDate,
            Gender = employeeDtoGet.Gender,
            HiredDate = employeeDtoGet.HiringDate,
            Email = employeeDtoGet.Email,
            PhoneNumber = employeeDtoGet.PhoneNumber,
            Status = employeeDtoGet.Status,
            GradeId = employeeDtoGet.GradeId,
            ProfileId = employeeDtoGet.ProfileId
        };
    }

    public static explicit operator EmployeeDtoGet(Employee employee)
    {
        return new EmployeeDtoGet
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
