using HW_7_8.Data.Models;
using HW_7_8.Data.Repositories;
using HW_7_8.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HW_7_8.Controllers
{
    [Authorize]
    [Route("/expenses")]
    public class ExpensesController : Controller
    {
        private readonly ExpenseRepository expensesRepository;
        private readonly CategoryRepository categoryRepository;

        public ExpensesController(ExpenseRepository expensesRepository, CategoryRepository categoryRepository)
        {
            this.expensesRepository = expensesRepository;
            this.categoryRepository = categoryRepository;
        }

        [Route("/")]
        public IActionResult Index()
        {
            ExpensesListViewModel model = new ExpensesListViewModel();
            DateTime currentDate = DateTime.Now;
            model.Month = currentDate.Month;
            model.Year = currentDate.Year;
            model.MonthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(model.Month);
            model.Expenses = expensesRepository.GetExpensesBy(model.Month, model.Year);
            model.TotalCost = expensesRepository.GetTotalCostByMonth(model.Month, model.Year);
            return View(model);
        }

        [HttpGet("/expenses/by-month")]
        public IActionResult MonthExpenses(string monthName, int year)
        {
            ExpensesListViewModel model = new ExpensesListViewModel();
            model.Year = year;
            model.MonthName = monthName;
            model.Month = DateTime.ParseExact(monthName, "MMMM", CultureInfo.InvariantCulture).Month;
            model.Expenses = expensesRepository.GetExpensesBy(model.Month, model.Year);
            model.TotalCost = expensesRepository.GetTotalCostByMonth(model.Month, model.Year);
            return View(model);
        }

        [HttpGet("/expenses/add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("/expenses/add")]
        public IActionResult Add(ExpenseAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                Expense expense = new Expense()
                {
                    Cost = (int)model.Cost,
                    Comment = model.Comment,
                    DateCreated = (DateTime)model.DateCreated,
                    ExpenseCategory = categoryRepository.GetCategoryById(Convert.ToInt32(model.SelectedCategoryId))
                };
                expensesRepository.Add(expense);
                var monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(expense.DateCreated.Month);
                return RedirectToAction("MonthExpenses", 
                    new { MonthName = monthName,
                    Year = expense.DateCreated.Year}) ;
            }
            return View();
        }

        [HttpGet("/expenses/edit/{id:int}")]
        public IActionResult Edit(int id) 
        {
            var expense = expensesRepository.GetExpenseById(id);
            ExpenseEditViewModel model = new ExpenseEditViewModel()
            {
                Id = expense.Id,
                Cost = expense.Cost,
                Comment = expense.Comment,
                DateCreated = expense.DateCreated,
                SelectedCategoryId = expense.ExpenseCategory.Id.ToString(),
                ReturnUrl = Request.Headers["Referer"].ToString()
            };
            return View(model);
        }

        [HttpPost("/expenses/edit/{id:int}")]
        public IActionResult Edit(ExpenseEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                var expense = expensesRepository.GetExpenseById(model.Id);
                expense.Cost = (int)model.Cost;
                expense.Comment = model.Comment;
                expense.ExpenseCategory = categoryRepository.GetCategoryById(Convert.ToInt32(model.SelectedCategoryId));
                expensesRepository.Update(expense);
                return Redirect(model.ReturnUrl);
            }
            return View(model);
        }

        [HttpPost("{id:int}")]
        public IActionResult Delete(int id)
        {
            expensesRepository.Delete(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}