using CompWebShopApp.Data;
using CompWebShopApp.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("MSSQLShopDb") ??
    throw new InvalidOperationException("You should provide connection string!");

builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddRazorPages();
builder.Services.AddIdentity<ShopUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
    }).AddEntityFrameworkStores<ShopContext>();
builder.Services.AddAuthentication().AddGoogle(options =>
{
    IConfigurationSection googleSection = builder.Configuration.GetSection("Authentication:Google");
    string clientId = googleSection.GetValue<string>("ClientId") ??
        throw new InvalidOperationException("Please provide ClientId!");
    string clientSecret = googleSection.GetValue<string>("ClientSecret") ??
        throw new InvalidOperationException("Please provide ClientSecret!");
    options.ClientId = clientId;
    options.ClientSecret = clientSecret;
});

builder.Services.AddAutoMapper(typeof(ShopUserProfile), typeof(RoleProfile));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
