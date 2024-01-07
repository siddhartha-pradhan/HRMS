using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class LeaveRequest : BaseEntity<Guid>
{
    public Guid RequestingEmployeeId { get; set; }

    public Guid LeaveTypeId { get; set; }

    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }

    public DateTime RequestedDate { get; set; }
    
    public string? RequestComments { get; set; }

    public bool IsActionComplete { get; set; }
    
    public bool IsApproved { get; set; }
    
    public bool IsCancelled { get; set; }

    [ForeignKey("LeaveTypeId")]
    public virtual LeaveType? LeaveType { get; set; }
    
    [ForeignKey("RequestingEmployeeId")]
    public virtual Employee? Employee { get; set; }
}