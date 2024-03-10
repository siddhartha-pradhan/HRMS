﻿using HRMS.Domain.Constants;

namespace HRMS.Application.DTOs.Accounts;

public class AccountDtoRegister
{
    public string FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public Gender Gender { get; set; }
    
    public DateTime HiringDate { get; set; }
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Password { get; set; }
    
    public string ConfirmPassword { get; set; }
}
