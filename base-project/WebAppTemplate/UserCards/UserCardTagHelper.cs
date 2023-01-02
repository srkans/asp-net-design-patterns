using Microsoft.AspNetCore.Razor.TagHelpers;
using WebAppTemplate.Models;

namespace WebAppTemplate.UserCards
{
    //<user-card app-user='' /> property attribute'a donusuyor.
    public class UserCardTagHelper :TagHelper
    {
        public AppUser AppUser { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserCardTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            
            _httpContextAccessor = httpContextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            UserCardTemplate userCardTemplate;

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                userCardTemplate = new PrimeUserCardTemplate();
            }
            else
            {
                userCardTemplate = new DefaultUserCardTemplate();
            }

            userCardTemplate.SetUser(AppUser);

            output.Content.SetHtmlContent(userCardTemplate.Build());
        }

        //public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output) //usercard template'inde gosterilecek kod burada yaziliyor
        //{
        //    UserCardTemplate userCardTemplate;

        //    if(_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        userCardTemplate = new PrimeUserCardTemplate();
        //    }
        //    else
        //    {
        //        userCardTemplate = new DefaultUserCardTemplate();
        //    }

        //    userCardTemplate.SetUser(AppUser);

        //    output.Content.SetHtmlContent(userCardTemplate.Build());
        //    return Task.CompletedTask;

        //}
    }
}
