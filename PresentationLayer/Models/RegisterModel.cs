using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class RegisterModel
    {
        [Required]
        [MinLength(3,ErrorMessage ="User Name Must be string And Min Length is 3")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage = "Minimum 8 characters")]
        [MaxLength(12)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$"
                          , ErrorMessage = "Should have at least one number\r\nShould have at least one upper case\r\nShould have at least one lower case\r\nShould have at least one special characte")]
        public string Password { get; set; }
    }
}
