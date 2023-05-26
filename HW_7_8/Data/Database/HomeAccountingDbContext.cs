using HW_7_8.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HW_7_8.Data.Database
{
    public class HomeAccountingDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public HomeAccountingDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Expense>().
                HasOne(e => e.ExpenseCategory)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomeAccountingDbContext).Assembly);
        }
    }
}