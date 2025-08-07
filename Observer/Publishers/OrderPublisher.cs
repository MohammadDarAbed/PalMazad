using MassTransit;
using Observer.Events;


namespace Observer.Publishers
{
    public interface IOrderPublisher
    {
        Task PublishOrderCreated(int orderId, decimal amount, string customerEmail);
    }

    public class OrderPublisher : IOrderPublisher
    {
        private readonly IBus _bus;

        public OrderPublisher(IBus bus)
        {
            _bus = bus;
        }

        public Task PublishOrderCreated(int orderId, decimal amount, string customerEmail)
        {
            Console.WriteLine("From Publish Order#: ", orderId);
            return _bus.Publish(new OrderCreated
            {
                OrderId = orderId,
                Amount = amount,
                CustomerEmail = customerEmail
            });
        }
    }
}