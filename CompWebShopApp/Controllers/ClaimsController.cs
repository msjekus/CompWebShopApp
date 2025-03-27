using CompWebShopApp.Data;
using CompWebShopApp.Model.ViewModels.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompWebShopApp.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly UserManager<ShopUser> userManager;
        private readonly SignInManager<ShopUser> signInManager;

        public ClaimsController(UserManager<ShopUser> userManager, SignInManager<ShopUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var claims = User.Claims;
                ShopUser? shopUser = await userManager.FindByNameAsync(User.Identity.Name!);
                if (shopUser == null) return NotFound();
                IndexClaimVM vM = new IndexClaimVM()
                {
                    Claims = claims,
                    UserName = shopUser.UserName!,
                    Email = shopUser.Email!
                };
                return View(vM);
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string claimType, string claimValue)
        {
            if (!ModelState.IsValid)return View();
            Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);
            ShopUser? shopUser = await userManager.GetUserAsync(User);
            if (shopUser == null) return NotFound();
            var res = await userManager.AddClaimAsync(shopUser, claim);
            if (res.Succeeded)
            {
                await signInManager.SignOutAsync();
                await signInManager.SignInAsync(shopUser, false);
                return RedirectToAction("Index");
            }
            Errors(ModelState, res);
            return View();
        }

        public void Errors(ModelStateDictionary modelState, IdentityResult identityResult)
        {
            foreach (var error in identityResult.Errors)
                modelState.AddModelError(string.Empty, error.Description);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string claimValues)
        {
            if (User != null && User.Identity != null)
            {
                string[] claimsD= claimValues.Split(';');
                string claimType = claimsD[0];
                string claimValue = claimsD[1];
                string claimIssuer = claimsD[2];
                Claim? claim = User.Claims.FirstOrDefault(t => t.Type == claimType && t.Value == claimValue && t.Issuer == claimIssuer);
                if (claim != null)
                {
                    ShopUser? shopUser = await userManager.GetUserAsync(User);
                    if (shopUser == null) return NotFound();
                    await userManager.RemoveClaimAsync(shopUser, claim);
                    await signInManager.SignOutAsync();
                    await signInManager.SignInAsync(shopUser, false);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Login", "Account");
        }        
    }
}
