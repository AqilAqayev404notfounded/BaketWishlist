using BaketWishlist.DataAcsessLayer;

namespace BaketWishlist.Models
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public Slider Slider { get; set; }
    }
}
