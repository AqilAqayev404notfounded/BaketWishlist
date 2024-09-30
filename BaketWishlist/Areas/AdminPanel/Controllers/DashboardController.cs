using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaketWishlist.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize]

    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
