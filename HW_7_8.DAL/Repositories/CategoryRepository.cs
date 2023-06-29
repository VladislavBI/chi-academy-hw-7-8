using HW_7_8.DAL.Database;
using HW_7_8.DAL.Entities;

namespace HW_7_8.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HomeAccountingDbContext dbContext;

        public CategoryRepository(HomeAccountingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Category GetCategoryById(int id) 
            => dbContext.Categories.SingleOrDefault(c => c.Id == id);

        public IEnumerable<Category> GetCategoriesByUserId(string userId)
            => dbContext.Categories.Where(c => c.User.Id == userId || c.User.Id == null);

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