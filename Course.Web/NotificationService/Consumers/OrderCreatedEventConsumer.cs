using MassTransit;
using Shared.Events;

namespace EmailService.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            Console.WriteLine($"Notification gönderildi : {context.Message.Email}");
            return Task.CompletedTask;
        }
    }
}
