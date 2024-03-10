using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.AccountRoles;

public class AccountRoleDtoUpdate
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public Guid RoleId { get; set; }

    public static implicit operator AccountRole(AccountRoleDtoUpdate accountRoleDtoUpdate)
    {
        return new AccountRole
        {
            Id = accountRoleDtoUpdate.Id,
            AccountId = accountRoleDtoUpdate.AccountId,
            RoleId = accountRoleDtoUpdate.RoleId,
            LastModifiedAt = DateTime.UtcNow
        };
    }

    public static explicit operator AccountRoleDtoUpdate(AccountRole accountRole)
    {
        return new AccountRoleDtoUpdate
        {
            Id = accountRole.Id,
            AccountId = accountRole.AccountId,
            RoleId = accountRole.RoleId
        };
    }
}
