using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppDecorator.Models;
using WebAppDecorator.Repositories;

namespace WebAppDecorator.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        //private readonly AppIdentityDbContext _context; //Dependency Inversion Ihlali (Dependency Injection)

        //public ProductsController(AppIdentityDbContext context)
        //{
        //    _context = context;
        //}

        
        

        private readonly IProductRepository _productRepository; //dependency inversion

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }




        // GET: Products
        public async Task<IActionResult> Index()
        {
              var userId = User.Claims.First(x=> x.Type == ClaimTypes.NameIdentifier).Value;

            return View(await _productRepository.GetAll(userId));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetById(id.Value); //nullable oldugu icin direkt value alindi      
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock")] Product product) //bind ile client'tan 10 data dahi gelse sadece belirtilenler alınıyor.
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            product.UserId = userId;
            await _productRepository.Save(product);
            return RedirectToAction(nameof(Index));
           
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Stock,UserId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productRepository.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                return Problem("Entity set 'AppIdentityDbContext.Products'  is null.");
            }
            else
            {
              await  _productRepository.Remove(product);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _productRepository.GetById(id) != null;
        }
    }
}
