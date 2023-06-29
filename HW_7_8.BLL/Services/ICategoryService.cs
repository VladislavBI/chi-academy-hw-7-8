using HW_7_8.BLL.Models;
using HW_7_8.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace HW_7_8.BLL.Services
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetAllByUserId(string userId);

        public CategoryDataModel GetCategoryById(int id);

        public void Add(CategoryDataModel category, IdentityUser user);

        public void Edit(CategoryDataModel newCategory);

        public void Delete(int id);
    }
}