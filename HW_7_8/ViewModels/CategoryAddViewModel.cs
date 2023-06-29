using System.ComponentModel.DataAnnotations;

namespace HW_7_8.ViewModels
{
    public class CategoryAddViewModel
    {
        [Required(ErrorMessage = "Required field")]
        public string Name { get; set; }

        public string? ReturnUrl { get; set; }
    }
}