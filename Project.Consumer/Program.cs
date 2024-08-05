using Core;
using MassTransit;
using MassTransitEventsDemo;
using Platform.Contracts.Events;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services.AddMassTransit
(
    x =>
    {
        x.AddConsumer<ProjectUserSavedEventConsumer>();

        x.UsingAzureServiceBus
        (
            (context, cfg) =>
            {
                cfg.Host(Config.AzureServiceBusConnectionString);
                cfg.Message<UserSavedEvent>(configTopology => configTopology.SetEntityName("events"));

                cfg.SubscriptionEndpoint<UserSavedEvent>
                (
                    "project-usersavedevent-subscription", e =>
                    {
                        e.ConfigureConsumer<ProjectUserSavedEventConsumer>(context);
                    }
                );
            }
        );
    }
);

var app = builder.Build();
app.Urls.Add("http://localhost:5003");
app.Run();