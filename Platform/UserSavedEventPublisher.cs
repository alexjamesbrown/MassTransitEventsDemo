using MassTransit;
using Platform.Contracts;
using Platform.Contracts.Events;

namespace Platform;

public class UserSavedEventPublisher(ILogger<UserSavedEventPublisher> logger, IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Press any key to publish a User Saved Event");

            await Task.Yield();

            var keyPressed = Console.ReadKey(true);

            if (keyPressed.Key != ConsoleKey.Escape)
            {
                var user = new User(Guid.NewGuid().ToString(), "John", "Doe");
                var userSavedEvent = new UserSavedEvent(user);

                await bus.Publish(userSavedEvent, stoppingToken);

                logger.LogInformation("User saved event published");
            }
        }
    }
}