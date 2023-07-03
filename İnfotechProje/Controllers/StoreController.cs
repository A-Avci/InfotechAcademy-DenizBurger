using Humanizer.Localisation;
using İnfotechProje.Models;
using İnfotechProje.View_Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace İnfotechProje.Controllers
{
    public class StoreController : Controller
    {
        private readonly İnfotechProjeEntities _context;

        public StoreController(İnfotechProjeEntities context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            List<Genre> genres = _context.Genres.ToList();
            ListViewModel model = new ListViewModel();
            model.ProductList = products;
            return View(model);
        }
        public IActionResult Details(int id)
        {
            var product = _context.Products.Where(x=>x.ProductId == id).FirstOrDefault();
            return View(product);
        }

        public IActionResult Browse(string genre)
        {
            //var genreModel = new Genre { Name = genre };
            //return View(genreModel);

            if (genre == "All")
            {
                List<Product> ürünler = _context.Products.ToList();
                return View(ürünler);
            }
            else
            {
                int genreid = _context.Genres.Where(p => p.Name == genre).FirstOrDefault().GenreId;
                List<Product> ürünler = _context.Products.Where(p => p.GenreId == genreid).ToList();
                return View(ürünler);
            }
        }
    }
}
