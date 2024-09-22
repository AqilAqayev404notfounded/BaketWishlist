using BaketWishlist.DataAcsessLayer;
using BaketWishlist.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class HeaderViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public HeaderViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var basket = Request.Cookies["basket"];

        if (string.IsNullOrEmpty(basket))
        {
            return View(new HeaderViewModel());
        }

        var basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);

        var newBaketViewModels = new List<BasketViewModel>();

        foreach (var item in basketViewModels)
        {
            var exsistProduct = _context.Products.Find(item.ProductId);

            if (exsistProduct == null)
            {
                continue;
            }

            newBaketViewModels.Add(new BasketViewModel
            {
                Name = exsistProduct.Name,
                ProductId = exsistProduct.Id,
                ImageUrl = exsistProduct.ImageUrl,
                Price = exsistProduct.Price,
                Count = item.Count
            });

        }
        var headerViewModel = new HeaderViewModel
        {
            Baket = newBaketViewModels,
            Count = newBaketViewModels.Sum(x => x.Count),
            Sum = newBaketViewModels.Sum(x => x.Count * x.Price)

        };
        return View(headerViewModel);
    }

    public async Task<IViewComponentResult> InvokeWishAsync()
    {
        var Wish = Request.Cookies["wishList"];

        if (string.IsNullOrEmpty(Wish))
        {
            return View(new HeaderViewModel());
        }

        var WishLIstViewModel = JsonConvert.DeserializeObject<List<WishListViewModel>>(Wish);

        var newWishViewModels = new List<WishListViewModel>();

        foreach (var item in WishLIstViewModel)
        {
            var exsistProduct = _context.Products.Find(item.ProductId);

            if (exsistProduct == null)
            {
                continue;
            }

            newWishViewModels.Add(new WishListViewModel
            {
                Name = exsistProduct.Name,
                ProductId = exsistProduct.Id,
                ImageUrl = exsistProduct.ImageUrl,
                Price = exsistProduct.Price
            });

        }
        var headerViewModel = new HeaderViewModel
        {
            WishList = newWishViewModels,
          

        };
        return View(headerViewModel);

    }
}
