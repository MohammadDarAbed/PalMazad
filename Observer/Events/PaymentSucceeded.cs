

namespace Observer.Events
{
    public record PaymentSucceeded
    {
        public int OrderId { get; init; }
        public DateTime PaymentDate { get; init; }
    }

}
