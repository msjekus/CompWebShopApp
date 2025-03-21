using Microsoft.AspNetCore.Identity;

namespace CompWebShopApp.Data
{
    public class ShopUser : IdentityUser
    {
        public DateOnly DateOfBirth { get; set; }
    }
}
