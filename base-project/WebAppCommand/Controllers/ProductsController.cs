using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCommand.Commands;
using WebAppCommand.Models;

namespace WebAppCommand.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public ProductsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> CreateFile(int type)
        {
            var products = await _context.Products.ToListAsync();

            FileCreateInvoker fileCreateInvoker= new FileCreateInvoker();
                      
            EFileType fileType = (EFileType)type;

            switch(fileType)
            {
                case EFileType.Excel:
                    ExcelFile<Product> excelFile = new(products);
                    fileCreateInvoker.SetCommand(new CreateExcelTableActionCommand<Product>(excelFile));
                    break;
                case EFileType.Pdf:
                    PdfFile<Product> pdfFile = new(products, HttpContext);
                    fileCreateInvoker.SetCommand(new CreatePdfTableActionCommand<Product>(pdfFile));
                    break;
                default: 

                    break;
            }

            return fileCreateInvoker.CreateFile();
        }
    }
}
