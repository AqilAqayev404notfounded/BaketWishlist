using BaketWishlist.DataAcsessLayer.Entity;

namespace BaketWishlist.DataAcsessLayer
{
    public class Category :BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> products { get; set; } = new List<Product>();
    }
}
