using CompWebShopApp.Data;
using CompWebShopApp.Model.DTOs.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompWebShopApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ShopUser> userManager;
        private readonly SignInManager<ShopUser> signInManager;

        public AccountController(UserManager<ShopUser> userManager,
            SignInManager<ShopUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register() =>  View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO dTO)
        {
            if (!ModelState.IsValid)
            {
                return View(dTO);
            }
            ShopUser shopUser = new ShopUser
            {
                UserName = dTO.UserName,
                Email = dTO.Email,
                DateOfBirth = dTO.DateOfBirth
            };
            {
               var res = await userManager.CreateAsync(shopUser, dTO.Password);
                if (res.Succeeded) 
                {
                    await signInManager.SignInAsync(shopUser, false);
                    return RedirectToAction ("Index", "Home");
                }
                else
                {
                    foreach (var error in res.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(dTO);
            }
        }
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO dTO)
        {
            if (!ModelState.IsValid)
            {
                return View(dTO);
            }
            ShopUser? user = await userManager.FindByNameAsync(dTO.UserName);
            if (user != null)
            {
                var res = await signInManager.PasswordSignInAsync(user, dTO.Password, dTO.RememberMe, false);
                if (res.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(string.Empty, "Логін або пароль користувача не вірний");
            }
            else
                ModelState.AddModelError(string.Empty, "Користувача не знайдено");
            return View(dTO);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
