using İnfotechProje.Extensions;
using İnfotechProje.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace İnfotechProje.Controllers
{
    public class CartController : Controller
    {
        private readonly İnfotechProjeEntities _context;
        public CartController(İnfotechProjeEntities context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(GetCart());
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Completed()
        {
            return View();
        }

        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.FirstOrDefault(i => i.ProductId == id);

            if (product != null)
            {
                var cart = GetCart();
                cart.AddProduct(product, 1);
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int Id)
        {
            var product = _context.Products.FirstOrDefault(i => i.ProductId == Id);

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }

            return RedirectToAction("Index");
        }

        public Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetObject<Cart>("Cart");
            if (cart == null)
            {
                cart = new Cart();
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            }
            return cart;
        }
    }
}
