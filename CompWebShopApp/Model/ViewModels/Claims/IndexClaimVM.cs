using System.Security.Claims;

namespace CompWebShopApp.Model.ViewModels.Claims
{
    public class IndexClaimVM
    {
        public IEnumerable<Claim>? Claims { get; set; }

        public string UserName { get; set; } = default!;

        public string Email { get; set; } = default!;
    }
}
