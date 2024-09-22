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

        //aye view bura baxir burdada model oturmemisen ordaki actionlari bura yazmaliyam? onun kimi birsey nece yeni gozle zulum

        public async Task<IActionResult> Index()
        {

            var categories = await _appDbContext.Categories.ToListAsync();
            var products = await _appDbContext.Products.ToListAsync();
            Response.Cookies.Append("cookie", "cookieValue", new CookieOptions { Expires = DateTimeOffset.Now.AddMinutes(5) });
            Response.Cookies.Append("cookieWL", "cookieValueWL", new CookieOptions { Expires = DateTimeOffset.Now.AddMinutes(15) });
            var model = new WishListViewModel() { Categories = categories, Products = products };

            return View(model);
        }

       
    }
}
