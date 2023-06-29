using HW_7_8.DAL.Database;
using HW_7_8.DAL.Entities;
using HW_7_8.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HW_7_8.DAL.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly HomeAccountingDbContext dbContext;

        public ExpenseRepository(HomeAccountingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Expense? GetExpenseById(int id)
            => dbContext.Expenses.Include(e => e.ExpenseCategory).SingleOrDefault(e => e.Id == id);

        public Expense? GetExpenseWithoutCategory(int id)
            => dbContext.Expenses.SingleOrDefault(e => e.Id == id);

        public IEnumerable<Expense> GetExpensesBy(int month, int year) 
            => dbContext.Expenses
            .Include(e => e.ExpenseCategory)
            .Where(e => e.DateCreated.Month == month && e.DateCreated.Year == year)
            .OrderByDescending(e => e.DateCreated);

        public IEnumerable<Expense> GetExpensesBy(int month, int year, string userId)
            => dbContext.Expenses
            .Include(e => e.ExpenseCategory)
            .Where(e => e.DateCreated.Month == month && e.DateCreated.Year == year && e.User.Id == userId)
            .OrderByDescending(e => e.DateCreated);

        public IEnumerable<Expense> GetExpensesBy(int month, int year, Category category) 
            => dbContext.Expenses
            .Include(e => e.ExpenseCategory)
            .Where(e => e.DateCreated.Month == month && e.DateCreated.Year == year && e.ExpenseCategory == category);

        public void Add(Expense expense)
        {
            dbContext.Expenses.Add(expense);
            dbContext.SaveChanges();
        }

        public void Delete (int id)
        {
            dbContext.Expenses.Remove(GetExpenseWithoutCategory(id));
            dbContext.SaveChanges();
        }

        public void Update (Expense expense)
        {
            dbContext.Expenses.Update(expense);
            dbContext.SaveChanges();
        }
    }
}