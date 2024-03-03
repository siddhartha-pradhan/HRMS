using System.ComponentModel.DataAnnotations.Schema;
using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class AccountRole : BaseEntity<Guid>
{
    public Guid AccountId { get; set; }
    
    public Guid RoleId { get; set; }
    
    [ForeignKey("AccountId")]
    public virtual Account Account { get; set; }
    
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }
}