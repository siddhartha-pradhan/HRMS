using HRMS.Domain.Entities;

namespace HRMS.Application.DTOs.Accounts;

public class AccountDtoGet
{
    public Guid Id { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int? OTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? ExpiredTime { get; set; }
    
    public static implicit operator Account(AccountDtoGet accountDtoGet)
    {
        return new Account
        {
            Id = accountDtoGet.Id,
            Password = accountDtoGet.Password,
            IsDeleted = accountDtoGet.IsDeleted,
            OTP = accountDtoGet.OTP,
            IsUsed = accountDtoGet.IsUsed,
            ExpiredTime = accountDtoGet.ExpiredTime
        };
    }
    
    public static explicit operator AccountDtoGet(Account account)
    {
        return new AccountDtoGet
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
