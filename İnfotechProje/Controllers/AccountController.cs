using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using İnfotechProje.Models;
using İnfotechProje.Data;

namespace İnfotechProje.Controllers
{
    public class AccountController : Controller
    {
        private readonly İnfotechProjeEntities _context;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AccountController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, İnfotechProjeEntities context)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Users users, string ReturnUrl)
        {
            var user = _context.Users.Where(x => x.Email == users.Email && x.Password == users.Password).FirstOrDefault();
            if (user == null)
            {
                //Add logic here to display some message to user    
                ViewBag.Message = "There is no such user.";
                return View(users);
            }
            else
            {
                //A claim is a statement about a subject by an issuer and    
                //represent attributes of the subject that are useful in the context of authentication and authorization operations.    
                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id))
                        //new Claim("FavoriteDrink", "Tea")
                    };
                //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                var principal = new ClaimsPrincipal(identity);
                //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                if (!String.IsNullOrEmpty(ReturnUrl))
                {
                    Redirect(ReturnUrl);
                }

                _httpContextAccessor.HttpContext.Session.SetString("Mail", user.Email);
                _httpContextAccessor.HttpContext.Session.SetString("Ad", user.Name);
                _httpContextAccessor.HttpContext.Session.SetString("Soyad", user.Surname);

                return RedirectToAction("Index", "Admin");
            }

            return View(users);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Users users, IFormFile files)
        {
            if (files != null && files.Length > 0)
            {
                var fileName = Path.GetFileName(files.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                users.Image = filePath;

                _context.Users.Add(users);
                _context.SaveChanges();

                using (var fileSrteam = new FileStream(filePath, FileMode.Create))
                {
                    files.CopyToAsync(fileSrteam);
                }
            }

            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return RedirectToAction("Login", "Account");
        }
    }
}
