using Core;
using MassTransit;
using Platform.Contracts.Events;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services.AddMassTransit
(
    x =>
    {
        x.AddConsumer<PricingUserSavedEventConsumer>();

        x.UsingAzureServiceBus
        (
            (context, cfg) =>
            {
                cfg.Host(Config.AzureServiceBusConnectionString);
                cfg.Message<UserSavedEvent>(configTopology => configTopology.SetEntityName("events"));

                cfg.SubscriptionEndpoint<UserSavedEvent>
                (
                    "pricing-usersavedevent-subscription", e =>
                    {
                        e.ConfigureConsumer<PricingUserSavedEventConsumer>(context);
                    }
                );
            }
        );
    }
);

var app = builder.Build();
app.Urls.Add("http://localhost:5002");
app.Run();