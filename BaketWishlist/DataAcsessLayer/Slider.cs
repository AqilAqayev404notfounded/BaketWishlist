using BaketWishlist.DataAcsessLayer.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaketWishlist.DataAcsessLayer
{
    public class Slider : BaseEntity
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public string Uptext { get; set; }
        public string MainTxt { get; set; }
        public string DownText { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
