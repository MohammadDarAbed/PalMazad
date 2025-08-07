
using MediatR;

namespace Observer.Events
{
    public record EmailEvent : INotification
    {
        public string To { get; init; } = string.Empty;
        public string Subject { get; init; } = string.Empty;
        public string Body { get; init; } = string.Empty;
    }
}
