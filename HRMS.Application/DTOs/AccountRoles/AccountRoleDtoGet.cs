using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.AccountRoles;

public class AccountRoleDtoGet
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public Guid RoleId { get; set; }

    public static implicit operator AccountRole(AccountRoleDtoGet accountRoleDtoGet)
    {
        return new AccountRole
        {
            Id = accountRoleDtoGet.Id,
            AccountId = accountRoleDtoGet.AccountId,
            RoleId = accountRoleDtoGet.RoleId
        };
    }

    public static explicit operator AccountRoleDtoGet(AccountRole accountRole)
    {
        return new AccountRoleDtoGet
        {
            Id = accountRole.Id,
            AccountId = accountRole.AccountId,
            RoleId = accountRole.RoleId
        };
    }
}
