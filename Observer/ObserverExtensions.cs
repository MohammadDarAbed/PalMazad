using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Observer.Publishers;
using PalMazad.Observer.Consumers;

namespace Observer
{
    public static class ObserverExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, string rabbitMqUri, string userName, string password)
        {
            services.AddMassTransit(x =>
            {
                // Register consumers
                x.AddConsumer<PaymentSucceededConsumer>();
                x.AddConsumer<PaymentFailedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqUri, h =>
                    {
                        h.Username(userName);
                        h.Password(password);
                    });

                    // Payment succeeded endpoint
                    cfg.ReceiveEndpoint("payment_succeeded_queue", e =>
                    {
                        e.ConfigureConsumer<PaymentSucceededConsumer>(context);
                    });

                    // Payment failed endpoint
                    cfg.ReceiveEndpoint("payment_failed_queue", e =>
                    {
                        e.ConfigureConsumer<PaymentFailedConsumer>(context);
                    });
                });
            });

            services.AddScoped<IOrderPublisher, OrderPublisher>();

            return services;
        }
    }
}
