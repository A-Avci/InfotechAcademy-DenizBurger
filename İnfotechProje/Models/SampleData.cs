using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Identity.Client;

namespace İnfotechProje.Models
{
    public class SampleData
    {
        private readonly İnfotechProjeEntities _context;


        public SampleData(İnfotechProjeEntities context)
        {
            _context = context;
        }

        public void AddGenres()
        {
            if (_context.Genres.ToList().Count == 0)
            {
                var genres = new List<Genre>
                {
                new Genre {Name="Hamburger",Description="Hamburger"},
                new Genre {Name="Pizza",Description="Pizza"},
                new Genre {Name="Pasta",Description="Pasta"},
                new Genre {Name="Fries",Description="Fries"},
                };

                foreach (var item in genres)
                {
                    _context.Genres.Add(item);
                }
                _context.SaveChanges();
            } 
        }

        public void AddProducts()
        {
            if (_context.Products.ToList().Count == 0)
            {
                var products = new List<Product>
                {
                    new Product { Title = "Delicious Burger", Genre = _context.Genres.Single(g => g.Name == "Hamburger"), Price =15, ProductArtUrl = "images/f2.png"},
                    new Product { Title = "Tasty Burger", Genre = _context.Genres.Single(g => g.Name == "Hamburger"), Price =12, ProductArtUrl = "images/f7.png" },
                    new Product { Title = "Grilled Tasty Burger", Genre = _context.Genres.Single(g => g.Name == "Hamburger"), Price =14, ProductArtUrl = "images/f8.png" },
                    new Product { Title = "Margarita", Genre = _context.Genres.Single(g => g.Name == "Pizza"), Price =20, ProductArtUrl ="images/f1.png"},
                    new Product { Title = "Delicious Pizza", Genre = _context.Genres.Single(g => g.Name == "Pizza"), Price =17, ProductArtUrl = "images/f3.png" },
                    new Product { Title = "Mixed Pizza", Genre = _context.Genres.Single(g => g.Name == "Pizza"), Price =15, ProductArtUrl = "images/f6.png" },
                    new Product { Title = "Delicious Pasta", Genre = _context.Genres.Single(g => g.Name == "Pasta"), Price =18, ProductArtUrl = "images/f4.png" },
                    new Product { Title = "Cheese Pasta", Genre = _context.Genres.Single(g => g.Name == "Pasta"), Price =10, ProductArtUrl = "images/f9.png" },
                    new Product { Title = "French Fries", Genre = _context.Genres.Single(g => g.Name == "Fries"), Price =10, ProductArtUrl = "images/f5.png" },
                };

                foreach (var item in products)
                {
                    _context.Products.Add(item);
                }
                _context.SaveChanges();
            }

        }

    }
    
}
