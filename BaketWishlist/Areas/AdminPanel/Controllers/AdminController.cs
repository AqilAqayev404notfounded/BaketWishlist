using Microsoft.AspNetCore.Mvc;

namespace BaketWishlist.Areas.AdminPanel.Controllers
{

    public class AdminController : Controller
    {
        [Area("AdminPanel")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
