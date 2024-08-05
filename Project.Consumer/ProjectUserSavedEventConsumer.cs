using MassTransit;
using Platform.Contracts.Events;

namespace MassTransitEventsDemo;

public class ProjectUserSavedEventConsumer : IConsumer<UserSavedEvent>
{
    public async Task Consume(ConsumeContext<UserSavedEvent> context)
    {
        var @event = context.Message;
        var user = @event.User;

        Console.WriteLine($"Project: User saved: {user.CsId} : {user.FirstName} {user.LastName}");

        await Task.CompletedTask;
    }
}