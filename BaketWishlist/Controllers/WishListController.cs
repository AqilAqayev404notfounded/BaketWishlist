using BaketWishlist.DataAcsessLayer;
using BaketWishlist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BaketWishlist.Controllers
{
    public class WishListController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public WishListController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<IActionResult> Index()
        {

            var categories = await _appDbContext.Categories.ToListAsync();
            var products = await _appDbContext.Products.ToListAsync();
            Response.Cookies.Append("cookie", "cookieValue", new CookieOptions { Expires = DateTimeOffset.Now.AddMinutes(5) });
            Response.Cookies.Append("cookieWL", "cookieValueWL", new CookieOptions { Expires = DateTimeOffset.Now.AddMinutes(15) });
            List<WishListViewModel> wishListViewModels = new List<WishListViewModel>();


            var wishlistByCookie = Request.Cookies["wishList"];

            if (wishlistByCookie is { })
                wishListViewModels = JsonConvert.DeserializeObject<List<WishListViewModel>>(wishlistByCookie) ?? new();

            var model = new HeaderViewModel() { Categories = categories, Products = products ,WishList=wishListViewModels};

            return View(model);
        }

       
    }
}
