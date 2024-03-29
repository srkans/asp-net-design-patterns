using WebAppObserver.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAppObserver.Observer;
using MediatR;
using System.Reflection;

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

builder.Services.AddMediatR(Assembly.GetExecutingAssembly()); //eventhandler

builder.Services.AddSingleton<UserObserverSubject>(sp =>
{
    UserObserverSubject userObserverSubject = new();

    userObserverSubject.RegisterObserver(new UserObserverWriteToConsole(sp));
    userObserverSubject.RegisterObserver(new UserObserverCreateDiscount(sp));
    // userObserverSubject.RegisterObserver(new UserObserverSendEmail(sp));

    return userObserverSubject;

});

var app = builder.Build();


using var scope = app.Services.CreateScope();
var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
identityDbContext.Database.Migrate(); //migrationlari otomatik uygula

if (!userManager.Users.Any())
{
  //  userManager.CreateAsync(new AppUser() { UserName = "serkan", Email = "serkansacma@hotmail.com" }, "Password12*").Wait(); //sadece uygulama ba�larken �al���yor
    userManager.CreateAsync(new AppUser() { UserName = "betul", Email = "betulaltu@hotmail.com" }, "Password12*").Wait(); //bu y�zden senkron olarak �a��r�ld�lar.
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
