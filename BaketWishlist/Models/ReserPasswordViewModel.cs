using System.ComponentModel.DataAnnotations;

namespace BaketWishlist.Models
{
    public class ReserPasswordViewModel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }  
        
    }
}
