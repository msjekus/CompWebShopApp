using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using CompWebShopDomainLibrary;
using CompWebShopApp.Model.DTOs.Admin;
using CompWebShopApp.Model.DTOs.Users;
using CompWebShopApp.Model.ViewModels.Users;
using CompWebShopApp.Model.DTOs.Roles;
namespace CompWebShopApp.Data
{
    public class ShopContext : IdentityDbContext<ShopUser>
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {}
        public DbSet<Brand> Brands { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<ProductImage> ProductImages { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;


        
        
    }
}
