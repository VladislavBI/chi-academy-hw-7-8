using HW_7_8.DAL.Entities;

namespace HW_7_8.DAL.Repositories
{
    public interface IExpenseRepository
    {
        public Expense? GetExpenseById(int id);

        public IEnumerable<Expense> GetExpensesBy(int month, int year);

        public IEnumerable<Expense> GetExpensesBy(int month, int year, string userId);

        public IEnumerable<Expense> GetExpensesBy(int month, int year, Category category);

        public void Add(Expense expense);

        public void Delete(int id);

        public void Update(Expense expense);
    }
}