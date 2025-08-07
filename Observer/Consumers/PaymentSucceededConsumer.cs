using MassTransit;
using MediatR;
using Observer.Events;
using System.Net.Http.Json;

namespace PalMazad.Observer.Consumers
{
    public class PaymentSucceededConsumer : IConsumer<PaymentSucceeded>
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        public PaymentSucceededConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task Consume(ConsumeContext<PaymentSucceeded> context)
        {
            var message = context.Message;
            Console.WriteLine($"[PaymentSucceededConsumer] Payment succeeded for Order {message.OrderId}");

            using var httpClient = new HttpClient();
            await httpClient.PutAsJsonAsync($"https://localhost:5001/orders/{message.OrderId}/payment-status",
                                            new { Status = "Paid" });

            await _mediator.Publish(new EmailEvent
            {
                To = "customer@example.com",  // Get from order buyer email
                Subject = "Payment Confirmation",
                Body = $"Your payment for order #{message.OrderId} was successful!"
            });
        }
    }
}
