using İnfotechProje.Models;
using Microsoft.AspNetCore.Mvc;

namespace İnfotechProje.Controllers
{
    public class AdminController : Controller
    {
        private readonly İnfotechProjeEntities _context;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AdminController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, İnfotechProjeEntities context)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUsers()
        {
            var list = _context.Users.ToList();
            return View(list);
        }

        public IActionResult GetProducts()
        {
            var list = _context.Products.ToList();
            return View(list);
        }

        public List<Genre> GetGenreList()
        {
            return _context.Genres.ToList();
        }

        public IActionResult CreateUpdateProduct(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.GenreList = GetGenreList();
                return View(new Product());
            }              
            else
            {
                var product = _context.Products.Find(id);
                if (product != null)
                {
                    ViewBag.GenreList = GetGenreList();
                    return View(product);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateProduct(Product products, IFormFile files)
        {
            if (files != null && files.Length > 0)
            {
                var fileName = Path.GetFileName(files.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                products.ProductArtUrl = fileName;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }   

                if (products.ProductId == 0)
                {
                    products.ProductArtUrl = fileName;
                    _context.Products.Add(products);
                }
                else
                {
                    products.ProductArtUrl = fileName;
                    _context.Products.Update(products);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.GenreList = GetGenreList();
            return View(products);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateUpdateUser(int id = 0)
        {
            if (id == 0)
                return View(new Users());
            else
                return View(_context.Users.Find(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdateUser(Users users, IFormFile files)
        {
            if (files != null && files.Length > 0)
            {
                var fileName = Path.GetFileName(files.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                users.Image = fileName;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }

                if (users.Id == 0)
                {
                    users.Image = fileName;
                    users.IsRole = "U";
                    _context.Users.Add(users);
                }
                else
                {
                    users.Image = fileName;
                    users.IsRole = users.IsRole;
                    _context.Users.Update(users);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(users);
        }

        public async Task<IActionResult> DeleteUser(int? id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
