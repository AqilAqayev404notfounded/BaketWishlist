using BaketWishlist.DataAcsessLayer;

namespace BaketWishlist.Models;

public class HeaderViewModel
{
    public List<BasketViewModel> Baket { get; set; } = new List<BasketViewModel>();
    public List<WishListViewModel> WishList { get; set; } = new List<WishListViewModel>();
    public int Count { get; set; }
    public decimal Sum { get; set; }
    public List<Category> Categories { get; set; } = new List<Category>();
    public List<Product> Products { get; set; } = new List<Product>();
}
