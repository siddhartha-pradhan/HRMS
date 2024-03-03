using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Base;
using HRMS.Domain.Constants;

namespace HRMS.Domain.Entities;

public class EmployeeProject : BaseEntity<Guid>
{
    public Guid EmployeeId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public ApprovalStatus ApprovalStatus { get; set; }
    
    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; set; }
    
    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; }
}