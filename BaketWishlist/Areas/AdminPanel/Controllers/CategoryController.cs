using BaketWishlist.DataAcsessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaketWishlist.Areas.AdminPanel.Controllers
{


    [Area("AdminPanel")]
    [Authorize]

    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task <IActionResult> Index()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return View(categories);
        }
    }
}
