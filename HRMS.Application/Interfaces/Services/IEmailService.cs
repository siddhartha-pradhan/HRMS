using HRMS.Application.DTOs.Email;

namespace HRMS.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendEmail(EmailActionDto emailAction);
}