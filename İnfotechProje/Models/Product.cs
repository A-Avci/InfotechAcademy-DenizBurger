using Humanizer.Localisation;

namespace İnfotechProje.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int GenreId { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public decimal Price { get; set; }
        public string ProductArtUrl { get; set; }
        
    }
}
