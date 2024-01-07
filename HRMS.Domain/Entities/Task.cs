using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Base;
using HRMS.Domain.Constants;
using HRMS.Domain.Entities.Identity;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace HRMS.Domain.Entities;

public class Task : BaseEntity<Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid? AssignedTo { get; set; }
    
    public Guid AssignedBy { get; set; }
    
    public int EstimatedHours { get; set; }

    public DateTime? StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }

    public TaskTypes Type { get; set; }
    
    public TaskStatus Status { get; set; }
    
    [ForeignKey("AssignedTo")]
    public virtual User AssignedToUser { get; set; }

    [ForeignKey("AssignedBy")]
    public virtual User AssignedByUser { get; set; }
}