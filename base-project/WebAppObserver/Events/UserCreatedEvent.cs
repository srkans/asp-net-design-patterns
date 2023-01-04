using MediatR;
using WebAppObserver.Models;

namespace WebAppObserver.Events
{
    public class UserCreatedEvent : INotification
    {
        public AppUser AppUser { get; set; }    
    }
}
