using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    private readonly IMailService _mailService;
    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                       ILogger<EmailSender> logger,
                       IMailService mailService,
                       Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
    {
        Options = optionsAccessor.Value;
        _logger = logger;
        _mailService = mailService;
        _env = env;
    }

    public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (_env.IsDevelopment())
        {
            // Mailtrap
            ExecuteByMailTrap(subject, message, toEmail);
            return;
        }

        if (string.IsNullOrEmpty(Options.SendGridKey))
        {
            throw new Exception("Null SendGridKey");
        }

        // SendGrid
        await Execute(Options.SendGridKey, subject, message, toEmail);
    }

    public async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("Joe@contoso.com", "Password Recovery"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);
        var response = await client.SendEmailAsync(msg);
        _logger.LogInformation(response.IsSuccessStatusCode 
                               ? $"Email to {toEmail} queued successfully!"
                               : $"Failure Email to {toEmail}");
    }

    public void ExecuteByMailTrap(string subject, string message, string toEmail)
    {
        _mailService.SendMail(new MailData
        {
            EmailToId = toEmail,
            EmailToName = string.Empty,
            EmailSubject = subject,
            EmailBody = message,
        });
    }
}