using AutoMapper;
using HW_7_8.BLL.Models;
using HW_7_8.BLL.Services;
using HW_7_8.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace HW_7_8.Controllers
{
    [Authorize]
    [Route("/expenses")]
    public class ExpensesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IExpenseService _expenseService;
        private readonly UserManager<IdentityUser> _userManager;

        public ExpensesController(IMapper mapper, IExpenseService expenseService, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _expenseService = expenseService;
            _userManager = userManager;
        }

        [Route("")]
        [Route("/")]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = _expenseService.GetCurrentByUserId(userId);
            ExpensesEnumerableViewModel model = _mapper.Map<ExpensesEnumerableViewModel>(expenses);
            return View(model);
        }

        [HttpGet("by-month")]
        public IActionResult MonthExpenses(string monthName, int year)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = _expenseService.GetByMonth(userId, monthName, year);
            ExpensesEnumerableViewModel model = _mapper.Map<ExpensesEnumerableViewModel>(expenses);
            return View(model);
        }

        [HttpGet("new")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("new")]
        public async Task<IActionResult> Add(ExpenseAddViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (ModelState.IsValid)
            {
                var expense = _mapper.Map<ExpenseAddModel>(model);
                _expenseService.Add(expense, user);

                var dateCreated = (DateTime)model.DateCreated;
                var monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(dateCreated.Month);
                
                return RedirectToAction("MonthExpenses", 
                    new { MonthName = monthName,
                    Year = dateCreated.Year}) ;
            }
            return View();
        }

        [HttpGet("{id:int}")]
        public IActionResult Edit(int id) 
        {
            var expense = _expenseService.GetById(id);
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

        [HttpPost("{id:int}")]
        public IActionResult Edit(ExpenseEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                var updatedExpense = _mapper.Map<ExpenseAddModel>(model);
                _expenseService.Edit(updatedExpense);
                return Redirect(model.ReturnUrl);
            }
            return View(model);
        }

        [HttpPost("{id:int}/delete")]
        public IActionResult Delete(int id)
        {
            _expenseService.Delete(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}