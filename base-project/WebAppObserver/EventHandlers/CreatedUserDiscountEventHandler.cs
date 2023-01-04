using MediatR;
using Microsoft.Extensions.Logging;
using WebAppObserver.Events;
using WebAppObserver.Models;

namespace WebAppObserver.EventHandlers
{
    public class CreatedUserDiscountEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<CreatedUserDiscountEventHandler> _logger;
        private readonly AppIdentityDbContext _context;

        public CreatedUserDiscountEventHandler(ILogger<CreatedUserDiscountEventHandler> logger, AppIdentityDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {

           await _context.Discounts.AddAsync(new Models.Discount { Rate = 10, UserId = notification.AppUser.Id });
           await _context.SaveChangesAsync();

            _logger.LogInformation("Discount created!");
          
        }
    }
}
