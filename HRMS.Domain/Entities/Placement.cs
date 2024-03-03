using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Placement : BaseEntity<Guid>
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public Guid EmployeeId { get; set; }
    
    public Guid DepartmentId { get; set; }
    
    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; }
}