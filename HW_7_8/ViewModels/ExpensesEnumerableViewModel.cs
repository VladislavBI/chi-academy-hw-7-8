using HW_7_8.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HW_7_8.ViewModels
{
    public class ExpensesEnumerableViewModel
    {
        public IEnumerable<Expense>? Expenses { get; set; }

        public double TotalCost { get; set; } = 0;

        public string MonthName { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public List<SelectListItem>? MonthNamesSelectList { get; set; }

        public List<SelectListItem>? YearsSelectList { get; set; }

    }
}