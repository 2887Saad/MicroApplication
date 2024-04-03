using Micro.Web.Models.Dto;
using Micro.Web.Services.IServices;
using Micro.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Micro.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _CategoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _CategoryService = categoryService;
        }
        [Authorize(Roles = SD.RoleCustomer)]
        public async Task<IActionResult> Index()
        {
            ResponseDto response = await _CategoryService.Get();
            if (response.IsSuccess && response.Result != null)
            {
                List<CategoryDto> categories = JsonConvert.DeserializeObject<List<CategoryDto>>(response.Result.ToString());
                return View(categories);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryDto categoryDto)
        {
            _CategoryService.Create(categoryDto);
            return View();
        }
        [HttpGet("[controller]/Update/{categoryId?}")]
        public async Task<IActionResult> Update(int categoryId)
        {
            ResponseDto response = await _CategoryService.GetById(categoryId);
            if (response.IsSuccess || response.Result != null)
            {
                CategoryDto? category = JsonConvert.DeserializeObject<CategoryDto>(response.Result.ToString());
                return View(category);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto CategoryDto)
        {
            var obj = await _CategoryService.Update(CategoryDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[controller]/Delete/{categoryId?}")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            ResponseDto response = await _CategoryService.GetById(categoryId);
            if (response.IsSuccess || response.Result != null)
            {
                CategoryDto? company = JsonConvert.DeserializeObject<CategoryDto>(response.Result.ToString());
                return View(company);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CategoryDto CategoryDto)
        {
            var obj = await _CategoryService.Delete(CategoryDto.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
