using CompWebShopApp.Data;
using CompWebShopApp.Model.ViewModels.Home;
using CompWebShopDomainLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CompWebShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopContext context;

        public HomeController(ShopContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            int itemsPerPage = 6;
            IQueryable<Product> products = context.Products
                .Include(t => t.Brand)
                .Include(t => t.Category)
                .Include(t => t.ProductImages);

            int productsCount = products.Count();
            int totalPages = (int)Math.Ceiling((float)productsCount / itemsPerPage);
            products = products
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage);
            HomeIndexVM vM = new HomeIndexVM()
            {
                
                CurrentPage = page,
                TotalPages = totalPages,
                Products = await products.ToListAsync()
            };
            return View(vM);

        }
    }
}
