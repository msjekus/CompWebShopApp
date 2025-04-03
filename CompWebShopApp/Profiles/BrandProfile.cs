using AutoMapper;
using CompWebShopApp.Model.DTOs.Brands;
using CompWebShopDomainLibrary;

namespace CompWebShopApp.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDTO>().ReverseMap();
        }
    }
}
