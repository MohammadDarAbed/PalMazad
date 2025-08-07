namespace Business.Email
{
    public interface IEmailManager
    {
        Task SendEmailAsync(string to, string subject, string body);
    }

    public class EmailManager : IEmailManager
    {
        private readonly IEmailService _emailService;

        public EmailManager(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            Console.WriteLine("From the EmailManager...");
            await _emailService.SendEmailAsync(to, subject, body);
        }
    }
}
