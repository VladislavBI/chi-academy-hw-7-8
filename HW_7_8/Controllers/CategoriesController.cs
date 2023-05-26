using HW_7_8.Data.Models;
using HW_7_8.Data.Repositories;
using HW_7_8.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HW_7_8.Controllers
{
    [Route("/categories")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly CategoryRepository categoryRepository;
        private readonly UserManager<IdentityUser> userManager;

        public CategoriesController(CategoryRepository categoryRepository, UserManager<IdentityUser> userManager)
        {
            this.categoryRepository = categoryRepository;
            this.userManager = userManager;
        }

        [Route("")]
        public IActionResult Index()
        {
            var model = new CategoriesListViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.Categories = categoryRepository.GetCategoriesByUserId(userId);
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
        public async Task<IActionResult> Add(CategoryAddViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                categoryRepository.Add(new Category() 
                { 
                    Name = model.Name,
                    User = await userManager.FindByIdAsync(userId)
                });

                return Redirect(model.ReturnUrl);
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