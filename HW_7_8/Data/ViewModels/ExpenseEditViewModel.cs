namespace HW_7_8.Data.ViewModels
{
    public class ExpenseEditViewModel : ExpenseAddViewModel
    {
        public int Id { get; set; }
        public string? ReturnUrl { get; set; }
    }
}