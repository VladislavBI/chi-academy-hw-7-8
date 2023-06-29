using HW_7_8.BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace HW_7_8.BLL.Services
{
    public interface IExpenseService
    {
        ExpensesEnumerableModel GetCurrentByUserId(string userId);

        ExpensesEnumerableModel GetByMonth(string userId, string monthName, int year);

        ExpenseDataModel GetById(int id);

        void Add(ExpenseAddModel expense, IdentityUser user);

        void Edit(ExpenseAddModel updatedExpense);

        void Delete(int id);

    }
}