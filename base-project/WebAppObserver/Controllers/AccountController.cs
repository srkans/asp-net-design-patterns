using WebAppObserver.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppObserver.Observer;

namespace WebAppObserver.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserObserverSubject _userObserverSubject;
         

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, UserObserverSubject userObserverSubject) //DI Container
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userObserverSubject = userObserverSubject;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email,string password)
        {
            var hasUser = await _userManager.FindByEmailAsync(email);

            if(hasUser == null) 
            {
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser,password,true,false);

            if(!signInResult.Succeeded)
            {
                return View();
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateViewModel userCreateViewModel)
        {
            var appUser = new AppUser { UserName = userCreateViewModel.UserName, Email=userCreateViewModel.Email };

           var identityResult = await _userManager.CreateAsync(appUser,userCreateViewModel.Password); 

            if (identityResult.Succeeded)
            {
                _userObserverSubject.NotifyObservers(appUser);
                ViewBag.message = "Üyelik işlemi başarıyla gerçekleşti. ";
            }
            else
            {
                ViewBag.message = identityResult.Errors.ToList().First().Description;
            }

            return View();
        }

    }
}
