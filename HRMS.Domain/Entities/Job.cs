using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Job : BaseEntity<Guid>
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public Guid DepartmentId { get; set; }
    
    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; }
    
    public virtual ICollection<EmployeeJob>? EmployeeJobs { get; set; }
}