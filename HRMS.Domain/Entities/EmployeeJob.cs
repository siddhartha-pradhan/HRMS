using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Base;
using HRMS.Domain.Constants;

namespace HRMS.Domain.Entities;

public class EmployeeJob : BaseEntity<Guid>
{
    public Guid EmployeeId { get; set; }
    
    public Guid InterviewId { get; set; }
    
    public Guid JobId { get; set; }
    
    public ApprovalStatus Status { get; set; }
    
    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("InterviewId")]
    public virtual Interview Interview { get; set; }

    [ForeignKey("JobId")]
    public virtual Job Job { get; set; }
}