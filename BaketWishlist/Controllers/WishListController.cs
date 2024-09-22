using BaketWishlist.DataAcsessLayer;
using BaketWishlist.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BaketWishlist.Controllers
{
    public class WishListController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
