using System.ComponentModel.DataAnnotations;

namespace HW_7_8.Data.ViewModels
{
    public class ExpenseAddViewModel
    {
        [Required(ErrorMessage = "Required field")]
        [Range(1, int.MaxValue)]
        public int? Cost { get; set; }

        [Required(ErrorMessage = "Required field")]
        public DateTime? DateCreated { get; set; }

        public string? Comment { get; set; }

        [Required(ErrorMessage = "Required field")]
        public string? SelectedCategoryId { get; set; }
    }
}