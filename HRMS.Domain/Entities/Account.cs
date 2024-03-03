using HRMS.Domain.Base;

namespace HRMS.Domain.Entities;

public class Account : BaseEntity<Guid>
{
    public string Password { get; set; }
    
    public int? OTP { get; set; }

    public bool IsUsed { get; set; } = false;
    
    public DateTime? ExpiredTime { get; set; }
    
    public ICollection<AccountRole>? AccountRoles { get; set; }
}