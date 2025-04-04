using CompWebShopApp.Model.DTOs.ProductImajges;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CompWebShopApp.Model.ViewModels.ImageProducts
{
    public class CreateImageVM
    {
        [Display(Name = "Бренди")]
        public SelectList? Brands { get; set; }
        [Display(Name = "Оберіть бренд")]
        public int? SelectedBrandId { get; set; }
        [Display(Name = "Категорії")]
        public SelectList? Categories { get; set; }
        [Display(Name = "Оберіть категорію")]
        public int? SelectedCategoryId { get; set; }
        [Display(Name = "Товари")]
        public SelectList? Products { get; set; }
        [Display(Name = "Оберіть товар")]
        public int SelectedProductId { get; set; }
        [Display(Name = "Зображення")]
        public IFormFile[] Photos { get; set; } = default!;
    }
}
