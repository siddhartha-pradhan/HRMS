using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Base;
using HRMS.Domain.Constants;
using HRMS.Domain.Entities.Identity;

namespace HRMS.Domain.Entities;

public class Employee : BaseEntity<Guid>
{
    public string Code { get; set; }
        
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public Gender Gender { get; set; }
    
    public DateTime HiredDate { get; set; }
    
    public ActionStatus Status { get; set; }
        
    public Guid GradeId { get; set; }
    
    public Guid ProfileId { get; set; }
    
    [ForeignKey("GradeId")]
    public Grade Grade { get; set; }

    [ForeignKey("ProfileId")]
    public Profile Profile { get; set; }
    
    public ICollection<EmployeeJob>? EmployeeJobs { get; set; }
    
    public ICollection<EmployeeProject>? EmployeeProjects { get; set; }
}