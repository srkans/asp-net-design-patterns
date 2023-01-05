using WebAppChain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WebAppChain.ChainOfResponsibility;

namespace WebAppChain.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppIdentityDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppIdentityDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> zipFile()
        {
            var products = await _context.Products.ToListAsync();

            var excelProcessHandler = new ExcelProcessHandler<Product>();

            var zipFileProcessHandler = new ZipFileProcessHandler<Product>();

            //var sendEmailProcessHandler = new SendEmailProcessHandler("product.zip", "serkansacma@hotmail.com");

            excelProcessHandler.SetNext(zipFileProcessHandler);

            excelProcessHandler.Handle(products);

            return View(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}