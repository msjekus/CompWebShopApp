using CompWebShopApp.Model.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace CompWebShopApp.Model.DTOs.ProductImajges
{
    public class ProductImageDTO
    {
        public int Id { get; set; }
        [Display(Name = "Назва зображення")]
        public byte[] ImageData { get; set; } = default!;
        [Display(Name = "Товар")]
        public int ProductId { get; set; }
        [Display(Name = "Товар")]
        public ProductDTO? Product { get; set; } = default!;
       
    }
}
