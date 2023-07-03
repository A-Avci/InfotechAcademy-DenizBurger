using İnfotechProje.Models;
using İnfotechProje.View_Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace İnfotechProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly İnfotechProjeEntities _context;

        public HomeController(ILogger<HomeController> logger, İnfotechProjeEntities context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            SampleData data = new SampleData(_context);
            data.AddGenres();
            data.AddProducts();
            List<Genre> genres = _context.Genres.ToList();
            List<Product> products = _context.Products.ToList();
            ListViewModel model = new ListViewModel();
            model.GenreList = genres;
            model.ProductList = products;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
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