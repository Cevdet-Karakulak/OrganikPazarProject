using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrganikPazar.Context;
using OrganikPazar.Entities;
using OrganikPazar.Helpers;
using OrganikPazar.Services.Interfaces;

namespace OrganikPazar.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGenericService<Product> _productService;
        private readonly IGenericService<Category> _categoryService;
        private readonly OrganikPazarContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(
            IGenericService<Product> productService,
            IGenericService<Category> categoryService,
            OrganikPazarContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _categoryService = categoryService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(string search = "", int page = 1, int pageSize = 25)
        {
            var query = _context.Products
                .Include(p => p.Category) 
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p =>
                    p.Productname.ToLower().Contains(search.ToLower()) ||
                    p.Category.Categoryname.ToLower().Contains(search.ToLower()));

            var totalCount = await query.CountAsync();

            var products = await query
                .OrderBy(p => p.Productname)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.Search = search;

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Categoryid", "Categoryname");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            await _productService.InsertAsync(product);
            await LoggerHelper.LogAsync(_context, _httpContextAccessor, "Create", nameof(Product), $"{product.Productname} ürünü eklendi.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Categoryid", "Categoryname", product.Categoryid);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            await _productService.UpdateAsync(product);
            await LoggerHelper.LogAsync(_context, _httpContextAccessor, "Update", nameof(Product), $"{product.Productname} ürünü güncellendi.");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.GetByIdAsync(id);
            await _productService.DeleteAsync(id);
            await LoggerHelper.LogAsync(_context, _httpContextAccessor, "Delete", nameof(Product), $"{deleted?.Productname} ürünü silindi.");
            return RedirectToAction("Index");
        }
    }
}
