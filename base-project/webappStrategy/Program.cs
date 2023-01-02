
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using webappStrategy.Models;
using webappStrategy.Repository;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.



var configuration = builder.Configuration; //.Net5 IConfiguration
var services = builder.Services;



//builder.Services.AddScoped<IProductRepository,ProductRepositoryFromSqlServer>(); sql server uzerinden calismak icin dinamik yontem degil

services.AddHttpContextAccessor(); //service provider uzerinden httpcontext'e erisebilmek icin

services.AddScoped<IProductRepository>(sp =>
{

    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();

    var claim = httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == Settings.claimDatabaseType).FirstOrDefault();



    var context = sp.GetRequiredService<AppIdentityDbContext>();
    if (claim == null) return new ProductRepositoryFromSqlServer(context);


    var databaseType = (EDatabaseType)int.Parse(claim.Value);



    return databaseType switch
    {
        EDatabaseType.SqlServer => new ProductRepositoryFromSqlServer(context),
        EDatabaseType.MongoDb => new ProductRepositoryFromMongoDb(configuration),
        _ => throw new NotImplementedException()
    };

});

services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
});

services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>();

services.AddControllersWithViews();

var app = builder.Build();
using var scope = app.Services.CreateScope();
var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
identityDbContext.Database.Migrate(); //migrationlari otomatik uygula

if (!userManager.Users.Any())
{
    userManager.CreateAsync(new AppUser() { UserName = "serkan", Email = "serkansacma@hotmail.com" }, "Password12*").Wait(); //sadece uygulama baþlarken 1 kez çalýþýyor
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

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
