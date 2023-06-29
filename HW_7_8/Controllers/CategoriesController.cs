using AutoMapper;
using HW_7_8.BLL.Models;
using HW_7_8.BLL.Services;
using HW_7_8.ViewModels;
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
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoriesController(ICategoryService categoryService, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var categories = _categoryService.GetAllByUserId(userId);
            var model = new CategoriesEnumerableViewModel()
            {
                Categories = categories,
            };
            return View(model);
        }

        [HttpGet("new")]
        public IActionResult Add()
        {
            var model = new CategoryAddViewModel()
            {
                ReturnUrl = Request.Headers["Referer"].ToString()
            };
            return View(model);
        }

        [HttpPost("new")]
        public async Task<IActionResult> Add(CategoryAddViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<CategoryDataModel>(model);
                _categoryService.Add(category, user);
                return Redirect(model.ReturnUrl);
            }
            return View(model); 
        }

        [HttpGet("{id:int}")]
        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            var model = _mapper.Map<CategoryEditViewModel>(category);
            return View(model);
        }

        [HttpPost("{id:int}")]
        public IActionResult Edit(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<CategoryDataModel>(model);
                _categoryService.Edit(category);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost("{id:int}/delete")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}