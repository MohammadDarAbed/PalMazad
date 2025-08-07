using MassTransit;
using PalMazad.Observer.Events;
using System.Net.Http.Json;

namespace PalMazad.Observer.Consumers
{
    public class PaymentFailedConsumer : IConsumer<PaymentFailed>
    {


        public async Task Consume(ConsumeContext<PaymentFailed> context)
        {
            var message = context.Message;
            Console.WriteLine($"[PaymentFailedConsumer] Payment failed for Order {message.OrderId}");

            using var httpClient = new HttpClient();
            await httpClient.PutAsJsonAsync($"https://palmazad-api/api/orders/{message.OrderId}/payment-status",
                                            new { Status = "NotPaid" });
        }
    }
}
