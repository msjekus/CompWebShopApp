using AutoMapper;
using CompWebShopApp.Model.DTOs.Roles;
using Microsoft.AspNetCore.Identity;

namespace CompWebShopApp.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleDTO>().ReverseMap();
        }
    }
}
