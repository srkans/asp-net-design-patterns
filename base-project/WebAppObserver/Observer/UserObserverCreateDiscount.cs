﻿using WebAppObserver.Models;

namespace WebAppObserver.Observer
{
    public class UserObserverCreateDiscount : IUserObserver //concrete observer B
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverCreateDiscount(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UserCreated(AppUser appUser) //async olarak tanımlarsan katkısı ne olur?
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();    

            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();



           context.Discounts.Add(new Models.Discount { Rate = 10, UserId = appUser.Id });
            context.SaveChanges();

            logger.LogInformation("Discount created!");
        }
    }
}
