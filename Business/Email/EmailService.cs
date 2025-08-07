using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Business.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        // SMTP Config
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 587; 
        private readonly string _smtpUser = "your-email@gmail.com";     
        private readonly string _smtpPass = "your-app-password";         

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true
            };

            using var mailMessage = new MailMessage(_smtpUser, to, subject, body)
            {
                IsBodyHtml = false
            };
            Console.WriteLine("The EMail Sent...");
            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch(Exception ex)
            {
                Console.WriteLine("This Error from the EmailServce: ", ex.Message);
            }
           
        }
    }
}
