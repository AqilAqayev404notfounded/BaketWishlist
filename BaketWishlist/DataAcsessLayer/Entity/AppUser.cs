using Microsoft.AspNetCore.Identity;

namespace BaketWishlist.DataAcsessLayer.Entity
{
    public class AppUser:IdentityUser
    {
        public string? FullName {  get; set; }
    }
}
