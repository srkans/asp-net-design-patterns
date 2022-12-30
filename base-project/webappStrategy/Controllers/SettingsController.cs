using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webappStrategy.Models;

namespace webappStrategy.Controllers
{
    public class SettingsController : Controller
    {
        [Authorize] //kullanıcı login olduysa calis
        public IActionResult Index()
        {
            Settings settings = new Settings();

           if(User.Claims.Where(x=>x.Type == Settings.claimDbType).FirstOrDefault()!=null)
            {
                settings.DataBaseType = (EDataBaseType)int.Parse(User.Claims.First(x => x.Type == Settings.claimDbType).Value);
            }
           else
            {
                settings.DataBaseType = settings.getDefaultDbType;
            }


            return View();
        }
    }
}
