using HRMS.Application.Utility.Handler;
using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Accounts;

public class AccountDtoUpdate
{
    public Guid Id { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int? OTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? ExpiredTime { get; set; }
    
    public static implicit operator Account(AccountDtoUpdate accountDtoUpdate)
    {
        return new Account
        {
            Id = accountDtoUpdate.Id,
            Password = HashingHandler.HashPassword(accountDtoUpdate.Password),
            IsDeleted = accountDtoUpdate.IsDeleted,
            OTP = accountDtoUpdate.OTP,
            IsUsed = accountDtoUpdate.IsUsed,
            ExpiredTime = accountDtoUpdate.ExpiredTime,
            LastModifiedAt = DateTime.UtcNow
        };
    }
    
    public static explicit operator AccountDtoUpdate(Account account)
    {
        return new AccountDtoUpdate
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
