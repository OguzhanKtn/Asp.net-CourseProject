using EmailService.Consumers;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedEventConsumer>();
    x.AddConsumer<UserCreatedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

        cfg.ReceiveEndpoint("email.service-order.created.event-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
            e.ConfigureConsumer<UserCreatedEventConsumer>(context);
        });
    });

});

var host = builder.Build();
host.Run();
