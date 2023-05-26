using HW_7_8.Data.Models;
using HW_7_8.Data.Repositories;
using HW_7_8.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HW_7_8.Controllers
{
    [Route("/categories")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly CategoryRepository categoryRepository;

        public CategoriesController(CategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            var model = new CategoriesListViewModel();
            model.Categories = categoryRepository.Categories;
            return View(model);
        }

        [HttpGet("/categories/add")]
        public IActionResult Add()
        {
            var model = new CategoryAddViewModel()
            {
                ReturnUrl = Request.Headers["Referer"].ToString()
            };
            return View(model);
        }

        [HttpPost("/categories/add")]
        public IActionResult Add(CategoryAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Add(new Category() { Name = model.Name });

                if (Url.IsLocalUrl(model.ReturnUrl))
                    return LocalRedirect(model.ReturnUrl);

                return RedirectToAction("Index");
            }
            return View(model); 
        }

        [HttpGet("/categories/edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var category = categoryRepository.GetCategoryById(id);
            var model = new CategoryEditViewModel()
            {
                Id = id,
                Name = category.Name,
            };
            return View(model);
        }

        [HttpPost("/categories/edit/{id:int}")]
        public IActionResult Edit(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = categoryRepository.GetCategoryById(model.Id);
                category.Name = model.Name;
                categoryRepository.Update(category);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost("{id:int}")]
        public IActionResult Delete(int id)
        {
            categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}