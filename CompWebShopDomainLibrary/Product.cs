using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompWebShopDomainLibrary
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Назва товару")]
        public string ProductName { get; set; } = default!;
        [Display(Name = "Опис товару")]
        public string Description { get; set; } = default!;
        [Display(Name = "Ціна товару")]
        public double Price { get; set; }
        
        public int BrandId { get; set; }
        [Display(Name = "Виробник")]
        public Brand Brand { get; set; } = default!;
        [Display(Name = "Категорія")]
        public Category Category { get; set; } = default!;
        public int CategoryId { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; } = default!;

    }
}
