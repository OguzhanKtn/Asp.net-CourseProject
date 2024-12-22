using MassTransit;
using Shared.Events;

namespace EmailService.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
