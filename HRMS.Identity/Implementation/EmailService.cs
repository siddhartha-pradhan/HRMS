using HRMS.Application.DTOs.Email;
using HRMS.Application.Interfaces.Services;
using HRMS.Domain.Constants;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace HRMS.Identity.Implementation;

public class EmailService : IEmailService
{
    private EmailOption _emailOption { get; }

    public EmailService(IOptions<EmailOption> emailSettings)
    {
        _emailOption = emailSettings.Value;
    }
    
    public async Task SendEmail(EmailActionDto emailAction)
    {
        var client = new MailjetClient(_emailOption.ApiKey, _emailOption.SecretKey);
        
        var request = new MailjetRequest
        {
            Resource = Send.Resource,
        }.Property(Send.Messages, new JArray { new JObject { 
            {
                "From",
                new JObject {
                    { "Email", "HRMS@mail.com"},
                    { "Name", "Futsal Fusion"}
                }
            }, 
            {
                "To",
                new JArray { 
                    new JObject { 
                        { "Email", emailAction.Email }, 
                        { "Name", emailAction.Email }
                    }
                }
            }, 
            {
                "Subject", emailAction.Subject },  
                {
                    "HTMLPart",
                    emailAction.Body
                },
            }
        });
        
        await client.PostAsync(request);
    }
}