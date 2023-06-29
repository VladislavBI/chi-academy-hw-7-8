using AutoMapper;
using HW_7_8.BLL.Models;
using HW_7_8.DAL.Entities;
using HW_7_8.DAL.Repositories;
using Microsoft.AspNetCore.Identity;

namespace HW_7_8.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAllByUserId(string userId)
        {
            return _categoryRepository.GetCategoriesByUserId(userId);
        }

        public CategoryDataModel GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            return _mapper.Map<CategoryDataModel>(category);
        }

        public void Add(CategoryDataModel category, IdentityUser user)
        {
            _categoryRepository.Add(new Category()
            {
                Name = category.Name,
                User = user
            });
        }

        public void Edit(CategoryDataModel newCategory)
        {
            var category = _mapper.Map<Category>(newCategory);
            _categoryRepository.Update(category);
        }

        public void Delete(int id)
        {
            _categoryRepository.Delete(id);
        }
    }
}