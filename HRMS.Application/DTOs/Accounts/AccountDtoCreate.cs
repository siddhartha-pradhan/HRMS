using HRMS.Domain.Entities;
using HRMS.Application.Utility.Handler;

namespace HRMS.Application.DTOs.Accounts;

public class AccountDtoCreate
{
    public Guid Id { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int? OTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? ExpiredTime { get; set; }

    public static implicit operator Account(AccountDtoCreate accountDtoCreate)
    {
        return new Account
        {
            Id = accountDtoCreate.Id,
            Password = HashingHandler.HashPassword(accountDtoCreate.Password),
            IsDeleted = accountDtoCreate.IsDeleted,
            OTP = accountDtoCreate.OTP,
            IsUsed = accountDtoCreate.IsUsed,
            ExpiredTime = accountDtoCreate.ExpiredTime,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static explicit operator AccountDtoCreate(Account account)
    {
        return new AccountDtoCreate
        {
            Id = account.Id,
            Password = account.Password,
            IsDeleted = account.IsDeleted,
            OTP = account.OTP,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime
        };
    }
}
