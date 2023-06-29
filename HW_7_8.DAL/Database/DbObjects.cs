using HW_7_8.DAL.Entities;

namespace HW_7_8.DAL.Database
{
    public class DbObjects
    {
        public static void Initial(HomeAccountingDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.AddRange(
                    new Category { Name = "None" },
                    new Category { Name = "Food" },
                    new Category { Name = "Transport" },
                    new Category { Name = "Communication" },
                    new Category { Name = "Internet" },
                    new Category { Name = "Entertainment" }
                    );
            }
            context.SaveChanges();
        }
    }
}