using WebAppComposite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

var app = builder.Build();
using var scope = app.Services.CreateScope();
var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
identityDbContext.Database.Migrate(); //migrationlari otomatik uygula

if (!userManager.Users.Any())
{
    var newUser = new AppUser() { UserName = "serkan", Email = "serkansacma@hotmail.com" };
    userManager.CreateAsync(newUser, "Password12*").Wait();                                                                         //sadece uygulama baþlarken çalýþýyor
    userManager.CreateAsync(new AppUser() { UserName = "betul", Email = "betulaltu@hotmail.com" }, "Password12*").Wait(); //bu yüzden senkron olarak çaðýrýldýlar.
    userManager.CreateAsync(new AppUser() { UserName = "user1", Email = "user1@hotmail.com" }, "Password12*").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "user2", Email = "user2@hotmail.com" }, "Password12*").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "user3", Email = "user3@hotmail.com" }, "Password12*").Wait();

    var newCategory1 = new Category { Name = "Sci-Fi", ReferenceId = 0, UserId = newUser.Id };
    var newCategory2 = new Category { Name = "Klasik", ReferenceId = 0, UserId = newUser.Id };
    var newCategory3 = new Category { Name = "Polisiye", ReferenceId = 0, UserId = newUser.Id };

    identityDbContext.Categories.AddRange(newCategory1,newCategory2, newCategory3);

    identityDbContext.SaveChanges(); //id'ler gelsin diye her ust kategoriden sonra savechanges kullandik

    var subCategory1 = new Category { Name = "Sci-Fi 1", ReferenceId = newCategory1.Id, UserId = newUser.Id };
    var subCategory2 = new Category { Name = "Klasik 1", ReferenceId = newCategory2.Id, UserId = newUser.Id };
    var subCategory3 = new Category { Name = "Polisiye 1", ReferenceId = newCategory3.Id, UserId = newUser.Id };


    identityDbContext.Categories.AddRange(subCategory1,subCategory2,subCategory3);
    identityDbContext.SaveChanges();

    var subCategory4 = new Category { Name = "Sci-Fi 1.1", ReferenceId = subCategory1.Id, UserId = newUser.Id };
    var subCategory5 = new Category { Name = "Klasik 1.1", ReferenceId = subCategory2.Id, UserId = newUser.Id };
    var subCategory6 = new Category { Name = "Polisiye 1.1", ReferenceId = subCategory3.Id, UserId = newUser.Id };

    identityDbContext.Categories.AddRange(subCategory4, subCategory5, subCategory6);
    identityDbContext.SaveChanges();
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
