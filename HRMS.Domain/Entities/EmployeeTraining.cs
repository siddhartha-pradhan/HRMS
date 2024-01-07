using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class EmployeeTraining : BaseEntity<Guid>
{
    public Guid EmployeeId { get; set; }
    
    public Guid TrainingId { get; set; }
    
    [ForeignKey("EmployeeId")]
    public virtual Employee? Employee { get; set; }

    [ForeignKey("TrainingId")]
    public virtual Training? Training { get; set; }
}