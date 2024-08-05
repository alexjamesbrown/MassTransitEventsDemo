using MassTransit;
using Platform.Contracts.Events;

public class PricingUserSavedEventConsumer : IConsumer<UserSavedEvent>
{
    public async Task Consume(ConsumeContext<UserSavedEvent> context)
    {
        var @event = context.Message;
        var user = @event.User;

        await Task.Delay(500);
        
        Console.WriteLine($"Pricing saving: {user.CsId}");
    }
}