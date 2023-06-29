using System.ComponentModel.DataAnnotations;

namespace HW_7_8.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Passwords do not match. Try again")]
        public string ConfirmPassword { get; set; }
    }
}