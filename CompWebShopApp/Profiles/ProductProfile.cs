using AutoMapper;
using CompWebShopApp.Model.DTOs.Products;
using CompWebShopDomainLibrary;

namespace CompWebShopApp.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }

}
