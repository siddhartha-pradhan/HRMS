using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Project : BaseEntity<Guid>
{
    public string Name { get; set; }
    
    public string Lead { get; set; }
    
    public string Description { get; set; }
    
    public ICollection<EmployeeProject>? EmployeeProjects { get; set; }
}