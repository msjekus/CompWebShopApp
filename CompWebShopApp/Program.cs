using CompWebShopApp.Data;
using CompWebShopApp.Profiles;
using CompWebShopApp.Requirements;
using Microsoft.AspNetCore.Authorization;
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
builder.Services.AddScoped<IAuthorizationRequirement, MinimalAgeRequirement>();
builder.Services.AddScoped<IAuthorizationHandler, MinimalAgeAuthorizationHandler>();
builder.Services.AddAuthentication().AddGoogle(options =>
{
    IConfigurationSection googleSection = builder.Configuration.GetSection("Authentication:Google");
    string clientId = googleSection.GetValue<string>("ClientId") ??
        throw new InvalidOperationException("Please provide ClientId!");
    string clientSecret = googleSection.GetValue<string>("ClientSecret") ??
        throw new InvalidOperationException("Please provide Client Secret!");
    options.ClientId = clientId;
    options.ClientSecret = clientSecret;
});
builder.Services.AddAuthorization(configure =>
{
    configure.AddPolicy("managerPolicy", policyBuilder =>
    {
        policyBuilder.RequireRole("Manager");
        policyBuilder.RequireRole("Hobbie", "Gardening");
    });
    configure.AddPolicy("hasAppropAge", policyBuilder =>
    {
        policyBuilder.RequireRole("Admin");
        policyBuilder.Requirements.Add(new MinimalAgeRequirement {MinimalAge = 18 });
    });
});
builder.Services.AddAutoMapper(typeof(ShopUserProfile), typeof(RoleProfile), typeof(BrandProfile));

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
