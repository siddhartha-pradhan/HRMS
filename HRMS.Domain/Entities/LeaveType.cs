using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class LeaveType : BaseEntity<Guid>
{
    public string Name { get; set; }
    
    public int DefaultDays { get; set; }
}