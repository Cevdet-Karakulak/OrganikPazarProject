using Microsoft.AspNetCore.Mvc;
using OrganikPazar.Context;
using OrganikPazar.Entities;
using OrganikPazar.Helpers;
using OrganikPazar.Services.Interfaces;

namespace OrganikPazar.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IGenericService<Category> _categoryService;
        private readonly OrganikPazarContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryController(
            IGenericService<Category> categoryService,
            OrganikPazarContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _categoryService = categoryService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            await _categoryService.InsertAsync(category);
            await LoggerHelper.LogAsync(_context, _httpContextAccessor, "Create", nameof(Category), $"{category.Categoryname} kategorisi eklendi.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            await _categoryService.UpdateAsync(category);
            await LoggerHelper.LogAsync(_context, _httpContextAccessor, "Update", nameof(Category), $"{category.Categoryname} kategorisi güncellendi.");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.GetByIdAsync(id);
            await _categoryService.DeleteAsync(id);
            await LoggerHelper.LogAsync(_context, _httpContextAccessor, "Delete", nameof(Category), $"{deleted?.Categoryname} kategorisi silindi.");
            return RedirectToAction("Index");
        }
    }
}
