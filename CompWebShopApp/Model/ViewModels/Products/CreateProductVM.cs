using CompWebShopApp.Model.DTOs.Products;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompWebShopApp.Model.ViewModels.Products
{
    public class CreateProductVM
    {
        public ProductDTO ProductDTO { get; set; } = default!;

        public SelectList? Brands { get; set; }

        public SelectList? Categories { get; set; }
    }
}
