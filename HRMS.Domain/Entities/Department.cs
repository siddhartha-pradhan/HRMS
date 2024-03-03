using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Department : BaseEntity<Guid>
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public Guid? ManagerId { get; set; }
    
    [ForeignKey("ManagerId")]
    public virtual Employee Employee { get; set; }
    
    public ICollection<Job>? Jobs { get; set; }
    
    public ICollection<Placement>? Placements { get; set; }
}