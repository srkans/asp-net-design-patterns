using MediatR;
using Microsoft.Extensions.Logging;
using WebAppObserver.Events;

namespace WebAppObserver.EventHandlers
{
    public class CreatedUserWriteConsoleEventHandler : INotificationHandler<UserCreatedEvent> 
    {
        private readonly ILogger<CreatedUserWriteConsoleEventHandler> _logger; //service provider kullanmaya gerek yok

        public CreatedUserWriteConsoleEventHandler(ILogger<CreatedUserWriteConsoleEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"user created : Id = {notification.AppUser.Id}");

            return Task.CompletedTask;
        }
    }
}
