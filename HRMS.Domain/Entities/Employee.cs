using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Base;
using HRMS.Domain.Entities.Identity;

namespace HRMS.Domain.Entities;

public class Employee : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    
    public string EmployeeReference { get; set; }
    
    public DateTime JoinedDate { get; set; }

    public DateTime? DepartedDate { get; set; }
    
    public Guid JobId { get; set; }

    public Guid DepartmentId { get; set; }
    
    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
    
    [ForeignKey("JobId")]
    public virtual JobTitle? Job { get; set; }
    
    [ForeignKey("DepartmentId")]
    public virtual Department? Department { get; set; }
}