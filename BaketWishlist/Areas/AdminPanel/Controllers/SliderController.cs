﻿using BaketWishlist.DataAcsessLayer;
using BaketWishlist.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaketWishlist.Areas.AdminPanel.Controllers
{
    [Authorize]

    public class SliderController : AdminController
    {

        private readonly AppDbContext _context;

        public SliderController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        private readonly IWebHostEnvironment _webHostEnvironment;
        public async Task<IActionResult> Index()
        {
             var sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }

            if (!slider.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Sekil secmelisiz");

                return View(slider);
            }

            if (!slider.ImageFile.IsValidSize(1))
            {
                ModelState.AddModelError("ImageFile", "Sekil olcusu max 1mb olmalidir");

                return View(slider);
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "fashion", "home-banner");
            var image = await slider.ImageFile.GenerateFileAsync(path);

            slider.ImageUrl = image;


            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
