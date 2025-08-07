namespace Observer.Events;

public record OrderCreated
{
    public int OrderId { get; init; }
    public decimal Amount { get; init; }
    public string CustomerEmail { get; init; } = string.Empty;
}
