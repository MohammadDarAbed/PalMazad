namespace PalMazad.Observer.Events
{
    public record PaymentFailed
    {
        public int OrderId { get; init; }
        public string Reason { get; init; } = string.Empty;
    }
}
