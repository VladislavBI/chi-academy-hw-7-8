using HW_7_8.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HW_7_8.DAL.Database
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