using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Role : BaseEntity<Guid>
{
    public string Name { get; set; }
    
    public ICollection<AccountRole>? AccountRoles { get; set; }
}