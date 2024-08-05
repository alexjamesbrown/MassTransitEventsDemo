using MassTransit;
using Core;
using Platform;
using Platform.Contracts.Events;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<UserSavedEventPublisher>();

builder.Services.AddMassTransit
(
    x =>
    {
        x.UsingAzureServiceBus
        (
            (context, cfg) =>
            {
                cfg.Host(Config.AzureServiceBusConnectionString);
                cfg.Message<UserSavedEvent>(configTopology => configTopology.SetEntityName("events"));
            }
        );
    }
);

var app = builder.Build();
app.Urls.Add("http://localhost:5001");
app.Run();