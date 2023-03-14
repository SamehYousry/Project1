using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class RegisterProvider
    {
        public IFormFile ImageProvider { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }

        [Required, MinLength(8, ErrorMessage = "Please enter at least 8 characters")]
        public string Password { get; set; }

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Service { get; set; }
        public IFormFile ImageFront { get; set; }
        public IFormFile ImageBack { get; set; }
        public int AdminId { get; set; }

    }
}
