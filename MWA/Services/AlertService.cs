using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MWA.Services
{
    public class AlertService
    {
        private readonly IConfiguration _config;

        public AlertService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAlertEmailAsync(string toEmail, string city, string alertMessage)
        {
            var smtpSection = _config.GetSection("SMTP");
            Console.WriteLine($"✅ Alert email sent to {toEmail} for {city}");

            var smtpClient = new SmtpClient(smtpSection["Host"], int.Parse(smtpSection["Port"]))
            {
                Credentials = new NetworkCredential(smtpSection["Username"], smtpSection["Password"]),
                EnableSsl = bool.Parse(smtpSection["EnableSsl"] ?? "true")
            };

            var mail = new MailMessage
            {
                From = new MailAddress(smtpSection["Username"], "MWA Weather Alerts"),
                Subject = $"⚠️ Weather Alert for {city}",
                Body = alertMessage
            };

            mail.To.Add(toEmail);

            await smtpClient.SendMailAsync(mail);
        }
    }
}