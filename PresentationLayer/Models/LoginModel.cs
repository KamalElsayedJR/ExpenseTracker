using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage ="Password Must Be Atleast 8 Chatacters")]
        public string Password { get; set; }
    }
}
