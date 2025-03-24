using AutoMapper;
using CompWebShopApp.Data;
using CompWebShopApp.Model.DTOs.Users;

namespace CompWebShopApp.Profiles
{
    public class ShopUserProfile : Profile
    {
        public ShopUserProfile()
        {
            CreateMap<ShopUser, ShopUserDTO>().ReverseMap();
        }
    }
}
