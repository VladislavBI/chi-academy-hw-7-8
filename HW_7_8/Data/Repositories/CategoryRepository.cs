using HW_7_8.Data.Database;
using HW_7_8.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW_7_8.Data.Repositories
{
    public class CategoryRepository
    {
        private readonly HomeAccountingDbContext dbContext;

        public CategoryRepository(HomeAccountingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public CategoryRepository()
        {
        }

        public IEnumerable<Category> Categories => dbContext.Categories;

        public Category GetCategoryById(int id) 
            => dbContext.Categories.SingleOrDefault(c => c.Id == id);

        public void Add(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            dbContext.Categories.Remove(GetCategoryById(id));
            dbContext.SaveChanges();
        }

        public void Update(Category category)
        {
            dbContext.Categories.Update(category);
            dbContext.SaveChanges();
        }
    }
}