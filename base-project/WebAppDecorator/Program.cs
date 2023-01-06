using WebAppDecorator.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAppDecorator.Repositories;
using WebAppDecorator.Repositories.Decorator;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddHttpsRedirection(options =>
//{
//    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
//    options.HttpsPort = 5001;
//});

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail= true;
}).AddEntityFrameworkStores<AppIdentityDbContext>();


builder.Services.AddMemoryCache();//**
//builder.Services.AddScoped<IProductRepository, ProductRepository>(); //Problem fixed InvalidOperationException: Unable to resolve service for type
builder.Services.AddScoped<IProductRepository>(sp=>
{
    var context = sp.GetRequiredService<AppIdentityDbContext>();
    var memoryCache = sp.GetRequiredService<IMemoryCache>();

    var productRepository = new ProductRepository(context);

    var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache);

    return cacheDecorator; //product repository yerine prcachedecorator nesne ornegi return edildi
    //class'larda ya da repository'de degisiklik yapmadan cache uzerinden calismayi sagladik
});


var app = builder.Build();

using var scope = app.Services.CreateScope();
var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
identityDbContext.Database.Migrate(); //migrationlari otomatik uygula

if (!userManager.Users.Any())
{
    userManager.CreateAsync(new AppUser() { UserName = "serkan", Email = "serkansacma@hotmail.com" }, "Password12*").Wait(); //sadece uygulama baþlarken çalýþýyor
    userManager.CreateAsync(new AppUser() { UserName = "betul", Email = "betulaltu@hotmail.com" }, "Password12*").Wait(); //bu yüzden senkron olarak çaðýrýldýlar.
    userManager.CreateAsync(new AppUser() { UserName = "user1", Email = "user1@hotmail.com" }, "Password12*").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "user2", Email = "user2@hotmail.com" }, "Password12*").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "user3", Email = "user3@hotmail.com" }, "Password12*").Wait();
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//if (app.Environment.IsDevelopment())
//{
//    app.UseHttpsRedirection();
//}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
