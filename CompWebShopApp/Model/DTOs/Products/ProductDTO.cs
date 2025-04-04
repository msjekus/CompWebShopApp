using CompWebShopApp.Model.DTOs.Brands;
using CompWebShopApp.Model.DTOs.Categories;
using CompWebShopDomainLibrary;
using System.ComponentModel.DataAnnotations;

namespace CompWebShopApp.Model.DTOs.Products
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Display(Name = "Назва товару")]
        public string ProductName { get; set; } = default!;
        [Display(Name = "Опис товару")]
        public string Description { get; set; } = default!;
        [Display(Name = "Ціна товару")]
        public double Price { get; set; }
        [Display(Name = "Виробник")]
        public int BrandId { get; set; }
        [Display(Name = "Виробник")]
        public BrandDTO? Brand { get; set; } = default!;
        [Display(Name = "Категорія")]
        public CategoryDTO? Category { get; set; } = default!;
        [Display(Name = "Категорія")]
        public int CategoryId { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; } = default!;
    }
}
