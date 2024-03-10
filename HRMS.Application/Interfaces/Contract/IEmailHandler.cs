namespace HRMS.Application.Interfaces.Contract;

public interface IEmailHandler
{
    void SendEmail(string toEmail, string subject, string htmlMessage);
}
