using MediatR;
using Observer.Events;

namespace Business.Email
{
    public class EmailEventHandler : INotificationHandler<EmailEvent>
    {
        private readonly IEmailManager _emailManager;

        public EmailEventHandler(IEmailManager emailManager)
        {
            _emailManager = emailManager;
        }

        public async Task Handle(EmailEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("From Email Handler...");
            await _emailManager.SendEmailAsync(notification.To, notification.Subject, notification.Body);
        }
    }
}
