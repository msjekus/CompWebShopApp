using AutoMapper;
using CompWebShopApp.Model.DTOs.Categories;
using CompWebShopDomainLibrary;

namespace CompWebShopApp.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
