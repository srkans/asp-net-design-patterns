using WebAppObserver.Models;

namespace WebAppObserver.Observer
{
    public interface IUserObserver
    {
        void UserCreated(AppUser appUser); //update
    }
}
