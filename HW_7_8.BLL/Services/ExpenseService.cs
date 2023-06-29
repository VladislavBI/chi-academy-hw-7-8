using AutoMapper;
using HW_7_8.BLL.Models;
using HW_7_8.DAL.Entities;
using HW_7_8.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace HW_7_8.BLL.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ExpenseService(IMapper mapper, IExpenseRepository expenseRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
        }

        public ExpensesEnumerableModel GetCurrentByUserId(string userId)
        {
            var model = new ExpensesEnumerableModel();
            model.Expenses = _expenseRepository.GetExpensesBy(model.Month, model.Year, userId);
            return model;
        }

        public ExpensesEnumerableModel GetByMonth(string userId, string monthName, int year)
        {
            var model = new ExpensesEnumerableModel(monthName, year);
            model.Expenses = _expenseRepository.GetExpensesBy(model.Month, model.Year, userId);
            return model;
        }

        public ExpenseDataModel GetById(int id)
        {
            var expense = _expenseRepository.GetExpenseById(id);
            return _mapper.Map<ExpenseDataModel>(expense);
        }

        public void Add(ExpenseAddModel expense, IdentityUser user)
        {
            _expenseRepository.Add(new Expense()
            {
                Cost = (int)expense.Cost,
                Comment = expense.Comment,
                DateCreated = (DateTime)expense.DateCreated,
                ExpenseCategory = _categoryRepository.GetCategoryById(Convert.ToInt32(expense.SelectedCategoryId)),
                User = user
            });
        }

        public void Edit(ExpenseAddModel updatedExpense)
        {
            var expense = _expenseRepository.GetExpenseById(updatedExpense.Id);
            expense.Cost = (int)updatedExpense.Cost;
            expense.Comment = updatedExpense.Comment;
            expense.ExpenseCategory = _categoryRepository.GetCategoryById(Convert.ToInt32(updatedExpense.SelectedCategoryId));
            _expenseRepository.Update(expense);
        }

        public void Delete(int id)
        {
            _expenseRepository.Delete(id);
        }
    }
}