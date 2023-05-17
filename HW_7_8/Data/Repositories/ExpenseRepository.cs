using HW_7_8.Data.Database;
using HW_7_8.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HW_7_8.Data.Repositories
{
    public class ExpenseRepository
    {
        private readonly HomeAccountingDbContext dbContext;

        public ExpenseRepository(HomeAccountingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Expense> Expenses => dbContext.Expenses.Include(e => e.ExpenseCategory);

        public Expense GetExpenseById(int id)
            => dbContext.Expenses.Include(e => e.ExpenseCategory).SingleOrDefault(e => e.Id == id);

        public IEnumerable<Expense> GetExpensesBy(int month, int year) 
            => dbContext.Expenses
            .Include(e => e.ExpenseCategory)
            .Where(e => e.DateCreated.Month == month && e.DateCreated.Year == year)
            .OrderByDescending(e => e.DateCreated);

        public IEnumerable<Expense> GetExpensesBy(int month, int year, Category category) 
            => dbContext.Expenses
            .Include(e => e.ExpenseCategory)
            .Where(e => e.DateCreated.Month == month && e.DateCreated.Year == year && e.ExpenseCategory == category);

        public int GetTotalCostByMonth(int month, int year)
            => dbContext.Expenses.Where(e => e.DateCreated.Month == month && e.DateCreated.Year == year).Sum(e => e.Cost);

        public void Add(Expense expense)
        {
            dbContext.Expenses.Add(expense);
            dbContext.SaveChanges();
        }

        public void Delete (int id)
        {
            dbContext.Expenses.Remove(GetExpenseById(id));
            dbContext.SaveChanges();
        }

        public void Update (Expense expense)
        {
            dbContext.Expenses.Update(expense);
            dbContext.SaveChanges();
        }

    }
}