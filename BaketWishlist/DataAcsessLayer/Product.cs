﻿using BaketWishlist.DataAcsessLayer.Entity;

namespace BaketWishlist.DataAcsessLayer
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Count { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
