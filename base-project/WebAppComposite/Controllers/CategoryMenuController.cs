using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAppComposite.Composite;
using WebAppComposite.Models;

namespace WebAppComposite.Controllers
{
    [Authorize]
    public class CategoryMenuController : Controller
    {

        private readonly AppIdentityDbContext _context;

        public CategoryMenuController(AppIdentityDbContext context)
        {          
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //category => bookcomposite
            //book => bookcomponent
            var userId = User.Claims.First(x=> x.Type == ClaimTypes.NameIdentifier).Value;

            var categories = await _context.Categories.Include(x=>x.Books).Where(x=>x.UserId == userId).OrderBy(x=>x.Id).ToListAsync();

            var menu = GetMenus(categories, new Category { Name = "TopCategory", Id = 0 }, new BookComposite(0, "TopMenu"));

            ViewBag.menu = menu;

            return View();
        }

        //recursive method

        public BookComposite GetMenus(List<Category> categories,Category topCategory,BookComposite topBookComposite, BookComposite last = null)
        {
            categories.Where(x => x.ReferenceId == topCategory.Id).ToList().ForEach(categoryitem =>
            {
                var bookComposite = new BookComposite(categoryitem.Id, categoryitem.Name);

                categoryitem.Books.ToList().ForEach(bookItem =>
                {
                    bookComposite.Add(new BookComponent(bookItem.Id, bookItem.Name));
                });

                if(last != null)
                {
                    last.Add(bookComposite);
                }
                else
                {
                    topBookComposite.Add(bookComposite);
                }

                GetMenus(categories,categoryitem,topBookComposite,bookComposite); //recursive

            });

            return topBookComposite;
        }

        
    }
}
