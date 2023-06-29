using HW_7_8.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace HW_7_8.BLL.Models
{
    public class ExpenseDataModel
    {
        public int Id { get; set; }

        public int Cost { get; set; }

        public DateTime DateCreated { get; set; }

        public string? Comment { get; set; }

        public Category ExpenseCategory { get; set; }

        public IdentityUser User { get; set; }
    }
}