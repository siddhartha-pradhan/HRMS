﻿namespace HRMS.Application.DTOs.Accounts;

public class AccountDtoChangePassword
{
    public string Email { get; set; }
    
    public int Otp { get; set; }
    
    public string NewPassword { get; set; }
    
    public string ConfirmNewPassword { get; set; }
}
