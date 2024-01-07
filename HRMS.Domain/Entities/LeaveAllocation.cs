using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class LeaveAllocation : BaseEntity<Guid>
{
    public Guid EmployeeId { get; set; }

    public Guid LeaveTypeId { get; set; }
    
    public int NumberOfDays { get; set; }

    public int Period { get; set; }

    [ForeignKey("LeaveTypeId")]
    public virtual LeaveType? LeaveType { get; set; }
    
    [ForeignKey("EmployeeId")]
    public virtual Employee? Employee { get; set; }
}