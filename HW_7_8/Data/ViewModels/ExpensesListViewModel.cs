using HW_7_8.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace HW_7_8.Data.ViewModels
{
    public class ExpensesListViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; }

        public double TotalCost { get; set; } = 0;

        public string MonthName { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public List<SelectListItem>? MonthNamesSelectList { get; set; }

        public List<SelectListItem>? YearsSelectList { get; set; }

        public ExpensesListViewModel()
        {
            var monthNames = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.SkipLast(1).ToArray();
            MonthNamesSelectList = new List<SelectListItem>();
            foreach (var month in monthNames)
            {
                MonthNamesSelectList.Add(new SelectListItem { Text = month, Value = month });
            }

            var years = Enumerable.Range(2020, DateTime.Now.Year - 2020 + 1).OrderDescending();
            YearsSelectList = new List<SelectListItem>();
            foreach (var year in years)
            {
                YearsSelectList.Add(new SelectListItem { Text = year.ToString(), Value = year.ToString() });
            }
        }
    }
}