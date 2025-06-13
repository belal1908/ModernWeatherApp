using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MWA.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAlertEmailAsync(string toEmail, string city, string alert)
        {
            var smtpConfig = _config.GetSection("SMTP");
            var host = smtpConfig["Host"];
            var port = int.Parse(smtpConfig["Port"]);
            var enableSsl = bool.Parse(smtpConfig["EnableSsl"]);
            var username = smtpConfig["Username"];
            var password = smtpConfig["Password"];

            var message = new MailMessage
            {
                From = new MailAddress(username),
                Subject = $"⛈️ Weather Alert for {city}",
                Body = $"Alert for {city}: {alert}",
                IsBodyHtml = false
            };

            message.To.Add(toEmail);

            using var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = enableSsl
            };

            await smtp.SendMailAsync(message);
        }
    }
}