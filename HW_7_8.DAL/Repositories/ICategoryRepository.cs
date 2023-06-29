using HW_7_8.DAL.Entities;

namespace HW_7_8.DAL.Repositories
{
    public interface ICategoryRepository
    {
        public Category GetCategoryById(int id);

        public IEnumerable<Category> GetCategoriesByUserId(string userId);

        public void Add(Category category);

        public void Delete(int id);

        public void Update(Category category);
    }
}