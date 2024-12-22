using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
    });
});

var host = builder.Build();
host.Run();
