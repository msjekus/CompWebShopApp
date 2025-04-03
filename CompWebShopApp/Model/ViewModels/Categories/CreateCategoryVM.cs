using CompWebShopApp.Model.DTOs.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompWebShopApp.Model.ViewModels.Categories
{
    public class CreateCategoryVM
    {
        public CategoryDTO CategoryDTO { get; set; } = default!;

        public SelectList? ParentCategories { get; set; }
    }
}
