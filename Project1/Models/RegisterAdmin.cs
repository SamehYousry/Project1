using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class RegisterAdmin
    {
        public string Name { get; set; }
        public string Email { get; set; }

        [Required, MinLength(8, ErrorMessage = "Please enter at least 8 characters")]
        public string Password { get; set; }

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
