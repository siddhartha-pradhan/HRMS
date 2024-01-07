using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Department : BaseEntity<Guid>
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public Guid ManagerId { get; set; }
    
    public virtual Employee Employee { get; set; }
}