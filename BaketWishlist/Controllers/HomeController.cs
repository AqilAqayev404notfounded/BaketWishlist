using BaketWishlist;
using BaketWishlist.DataAcsessLayer;
using BaketWishlist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BaketWishlist.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            var products = await _appDbContext.Products.ToListAsync();
            Response.Cookies.Append("cookie", "cookieValue", new CookieOptions { Expires = DateTimeOffset.Now.AddMinutes(5) });

            var model = new HomeViewModel() { Categories = categories, Products = products };

            return View(model);
        }

        public IActionResult Basket()
        {
            var basket = Request.Cookies["basket"];
            if (basket == null)
            {
                return Json(new List<BasketViewModel>());
            }

            var products = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);
            return Json(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket(int id)
        {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null) return BadRequest();

            var basket = Request.Cookies["basket"];
            List<BasketViewModel> basketItems;

            if (string.IsNullOrEmpty(basket))
            {
                basketItems = new List<BasketViewModel>();
            }
            else
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);
            }

            var existingProduct = basketItems.FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct == null)
            {
                basketItems.Add(new BasketViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Count = 1 
                });
            }
            else
            {
                existingProduct.Count++;
            }

            var basketJson = JsonConvert.SerializeObject(basketItems);
            Response.Cookies.Append("basket", basketJson, new CookieOptions { Expires = DateTimeOffset.Now.AddMinutes(30) });

            return Ok(); 
        }

    }
}

