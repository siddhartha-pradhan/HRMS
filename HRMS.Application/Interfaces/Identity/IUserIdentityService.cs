﻿using HRMS.Application.DTOs.Identity;

namespace HRMS.Application.Interfaces.Identity;

public interface IUserIdentityService
{
    Task<Tuple<string, string>> Register(RegisterDto register, string? returnUrl = null);

    Task<bool> ConfirmEmail(Guid userId, string code);

    Task<string> Login(LoginDto login, string? returnUrl = null);

    Task LogOut();

    Task<Tuple<string, string>> ForgetPassword(ForgotPasswordDto forgotPassword);

    Task<string> ResetPassword(ResetPasswordDto resetPassword);
}