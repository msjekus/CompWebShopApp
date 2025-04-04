using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompWebShopDomainLibrary
{
    public class Brand
    {
        public int Id { get; set; }
        [Display(Name = "Назва бренду")]
        public string BrandName { get; set; }= default!;
        [Display(Name = "Країна реєстрації бренду")]
        public string Country { get; set; } = default!;
        public ICollection<Product> Products { get; set; } = default!;
    }
}
