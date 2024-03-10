using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.AccountRoles;

public class AccountRoleDtoCreate
{
    public Guid AccountId { get; set; }
    public  Guid RoleId { get; set; }

    public static implicit operator AccountRole(AccountRoleDtoCreate accountRoleDtoCreate)
    {
        return new AccountRole
        {
            AccountId = accountRoleDtoCreate.AccountId,
            RoleId = accountRoleDtoCreate.RoleId,
            CreatedAt = DateTime.Now
        };
    }

    public static explicit operator AccountRoleDtoCreate(AccountRole accountRole)
    {
        return new AccountRoleDtoCreate
        {
            AccountId = accountRole.AccountId,
            RoleId = accountRole.RoleId,
        };
    }
}
