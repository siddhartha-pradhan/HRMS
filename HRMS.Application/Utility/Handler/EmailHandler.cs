using System.Net;
using System.Net.Mail;
using HRMS.Application.Interfaces.Contract;

namespace HRMS.Application.Utility.Handler;

public class EmailHandler : IEmailHandler
{
    private readonly int _smtpPort;
    private readonly string _smtpServer;
    private readonly string _fromEmailAddress;
    private readonly string _fromEmailPassword;

    public EmailHandler(string smtpServer, int smtpPort, string fromEmailAddress, string fromEmailPassword)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _fromEmailAddress = fromEmailAddress;
        _fromEmailPassword = fromEmailPassword;
    }

    public void SendEmail(string toEmail, string subject, string htmlMessage)
    {
        var message = new MailMessage
        {
            From = new MailAddress(_fromEmailAddress),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        message.To.Add(new MailAddress(toEmail));

        var client = new SmtpClient(_smtpServer)
        {
            Port = _smtpPort,
            Credentials = new NetworkCredential(_fromEmailAddress, _fromEmailPassword),
            EnableSsl = true,
        };
        
        client.Send(message);
    }
}
