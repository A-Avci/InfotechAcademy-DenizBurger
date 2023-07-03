using İnfotechProje.Models;

namespace İnfotechProje.View_Model
{
    public class ListViewModel
    {
        public List<Genre>? GenreList { get; set; }
        public List<Product>? ProductList { get; set; }
        public Product Product { get; set; }
    }
}
