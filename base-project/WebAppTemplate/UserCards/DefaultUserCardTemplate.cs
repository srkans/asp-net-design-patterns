namespace WebAppTemplate.UserCards
{
    public class DefaultUserCardTemplate : UserCardTemplate
    {


        protected override string SetFooter()
        {
           return string.Empty;
        }

        protected override string SetPicture()
        {
            return $"<img src ='/userPictures/man.jpg' class='card-img-top' alt='Card Image'>";
        }
    }
}
