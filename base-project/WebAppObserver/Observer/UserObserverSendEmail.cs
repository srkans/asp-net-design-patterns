using WebAppObserver.Models;

namespace WebAppObserver.Observer
{
    public class UserObserverSendEmail : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverSendEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateUser(AppUser appUser)
        {
           var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendEmail>>();
        }
    }
}
