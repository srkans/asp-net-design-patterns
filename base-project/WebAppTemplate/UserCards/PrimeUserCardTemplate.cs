using System.Text;
using WebAppTemplate.Models;

namespace WebAppTemplate.UserCards
{
    public class PrimeUserCardTemplate : UserCardTemplate
    {
        protected override string SetFooter()
        {
            var stringbuilder = new StringBuilder();

            stringbuilder.Append("<a href='#' class='card-link'>Mesaj Gönder</a>");
            stringbuilder.Append("<a href='#' class='card-link'>Profil Detayları</a>");

            return stringbuilder.ToString();
        }

        protected override string SetPicture()
        {
            return $"<img src ='{_appUser.PictureUrl}' class='card-img-top' alt='Card Image'>";
        }
    }
}
