using System.Text;
using WebAppTemplate.Models;

namespace WebAppTemplate.UserCards
{
    public abstract class UserCardTemplate
    {
        protected AppUser _appUser {get; set;}

        public void SetUser(AppUser appUser)
        {
            _appUser = appUser;
        }

        public string Build()
        {
            if (_appUser == null) throw new ArgumentNullException(nameof(_appUser));

            var stringbuilder = new StringBuilder();

            stringbuilder.Append("<div class='card'>");

            stringbuilder.Append(SetPicture());

            stringbuilder.Append($@"<div class='card-body'>
                                   <h5>{_appUser.UserName}</h5>
                                    <p>{_appUser.Description}</p>");

            stringbuilder.Append(SetFooter());
            stringbuilder.Append("</div>");
            stringbuilder.Append(" </div>");

            return stringbuilder.ToString();
        }


        protected abstract string SetFooter();

        protected abstract string SetPicture();

    }
}
