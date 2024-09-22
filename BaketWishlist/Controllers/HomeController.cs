using BaketWishlist;
using BaketWishlist.DataAcsessLayer;
using BaketWishlist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            Response.Cookies.Append("cookieWL", "cookieValueWL", new CookieOptions { Expires = DateTimeOffset.Now.AddMinutes(15) });
            var model = new HomeViewModel() { Categories = categories, Products = products };

            return View(model);
        }

        public IActionResult Basket()
        {
            //if (basket == null)
            //{
            //    return Json(new List<BasketViewModel>());
            //}
            var basket = Request.Cookies["basket"];

            var basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);

            var newBaketViewModels = new List<BasketViewModel>();

            foreach (var item in basketViewModels)
            {
                var exsistProduct = _appDbContext.Products.Find(item.ProductId);

                if (exsistProduct==null)
                {
                    continue;
                }

                newBaketViewModels.Add(new BasketViewModel
                {
                    Name=exsistProduct.Name,
                    ProductId = exsistProduct.Id,
                    ImageUrl = exsistProduct.ImageUrl,
                    Price = exsistProduct.Price,
                    Count = item.Count
                });

            }
            return Json(basketViewModels);
        }

        public async Task<IActionResult> AddToBasket(int id)
        {
            var product = await _appDbContext.Products.FindAsync(id);  
            if (product == null) return BadRequest();

            var basketViewModels = new List<BasketViewModel>();
            if (string.IsNullOrEmpty(Request.Cookies["basket"]))
            {
                basketViewModels.Add(new BasketViewModel
                {
                    Name = product.Name,
                    ProductId = product.Id,
                    ImageUrl= product.ImageUrl,
                    Price = product.Price,
                    Count = 1
                    
                });
            }
            else
            {
                basketViewModels =  JsonConvert.DeserializeObject<List<BasketViewModel>>(Request.Cookies["basket"]);
                var existProduct = basketViewModels.Find(x => x.ProductId == product.Id);
                if (existProduct==null)
                {
                    basketViewModels.Add(new BasketViewModel
                    {
                        Name = product.Name,
                        ProductId = product.Id,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,
                        Count = 1

                    });
                }
                else
                {
                    existProduct.Count++;
                    existProduct.Name = product.Name;
                    existProduct.Price = product.Price;
                    existProduct.ProductId = product.Id;
                    existProduct.ImageUrl = product.ImageUrl;
                }

            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketViewModels));


            return RedirectToAction(nameof(Index));

        }
        public IActionResult WishList()
        {

            var wishList = Request.Cookies["wishList"];

            var wishListViewModel = JsonConvert.DeserializeObject<List<WishListViewModel>>(wishList);

            var newWishListViewModels = new List<WishListViewModel>();

            foreach (var item in wishListViewModel)
            {
                var exsistProduct = _appDbContext.Products.Find(item.ProductId);

                if (exsistProduct == null)
                {
                    continue;
                }

                newWishListViewModels.Add(new WishListViewModel
                {
                    Name = exsistProduct.Name,
                    ProductId = exsistProduct.Id,
                    ImageUrl = exsistProduct.ImageUrl,
                    Price = exsistProduct.Price,
                });

            }
            return Json(wishListViewModel);

        }
        public async Task<IActionResult> AddToWishlIst(int id)
        {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null) return BadRequest();

            var wishlistViewModel = new List<WishListViewModel>();
            if (string.IsNullOrEmpty(Request.Cookies["wishList"]))
            {
                wishlistViewModel.Add(new WishListViewModel
                {
                    Name = product.Name,
                    ProductId = product.Id,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,

                });
            }
            else
            {
                wishlistViewModel = JsonConvert.DeserializeObject<List<WishListViewModel>>(Request.Cookies["basket"]);
                var existProduct = wishlistViewModel.Find(x => x.ProductId == product.Id);
                if (existProduct == null)
                {
                    wishlistViewModel.Add(new WishListViewModel
                    {
                        Name = product.Name,
                        ProductId = product.Id,
                        ImageUrl = product.ImageUrl,
                        Price = product.Price,

                    });
                }
                else
                {
                    existProduct.Name = product.Name;
                    existProduct.Price = product.Price;
                    existProduct.ProductId = product.Id;
                    existProduct.ImageUrl = product.ImageUrl;
                }

            }
            Response.Cookies.Append("wishList", JsonConvert.SerializeObject(wishlistViewModel));


            return RedirectToAction(nameof(Index));

        }
    }
}

