using System.Net;
using System.Net.Mail;
using WebAppObserver.Models;

namespace WebAppObserver.Observer
{
    public class UserObserverSendEmail : IUserObserver //concrete observer C
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverSendEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UserCreated(AppUser appUser)
        {
           var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendEmail>>();

            var mailMessage = new MailMessage();

            var smtpClient = new SmtpClient("smtp.gmail.com");

            mailMessage.From = new MailAddress("serkansacma@gmail.com");

            mailMessage.To.Add(new MailAddress(appUser.Email));

            mailMessage.Subject = "Mail Deneme";
            mailMessage.Body = "<p>Html formatında güzel bir mesaj yazılabilir</p>";
            mailMessage.IsBodyHtml= true;

            smtpClient.Port = 587;

            smtpClient.Credentials = new NetworkCredential("serkansacma@gmail.com", "password");

            smtpClient.Send(mailMessage);

            logger.LogInformation($"Email has been sent to user {appUser.UserName}");
        }
    }
}
