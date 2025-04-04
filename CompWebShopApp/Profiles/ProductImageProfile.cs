using AutoMapper;
using CompWebShopApp.Model.DTOs.ProductImajges;
using CompWebShopDomainLibrary;

namespace CompWebShopApp.Profiles
{
    public class ProductImageProfile : Profile
    {
        public ProductImageProfile()
        {
            CreateMap<ProductImage, ProductImageDTO>()
                .ReverseMap();
        }
    }
   
}
