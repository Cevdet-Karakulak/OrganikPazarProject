using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrganikPazar.Context;
using OrganikPazar.Entities;
using OrganikPazar.Helpers;
using OrganikPazar.Models.ViewModels;
using OrganikPazar.Services.Interfaces;

namespace OrganikPazar.Controllers
{
    public class OrderController : Controller
    {
        private readonly IGenericService<Order> _orderService;
        private readonly IGenericService<Customer> _customerService;
        private readonly IGenericService<Product> _productService;
        private readonly OrganikPazarContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(
            IGenericService<Order> orderService,
            IGenericService<Customer> customerService,
            IGenericService<Product> productService,
            OrganikPazarContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _customerService = customerService;
            _productService = productService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(string search = "", int page = 1, int pageSize = 25)
        {
            var query = _orderService.GetQueryable()
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                var lowered = search.ToLower();
                query = query.Where(o =>
                    (o.Customer.Firstname + " " + o.Customer.Lastname).ToLower().Contains(lowered) ||
                    o.Product.Productname.ToLower().Contains(lowered) ||
                    (o.City ?? "").ToLower().Contains(lowered));
            }

            var totalCount = await query.CountAsync();

            var orders = await query
                .OrderByDescending(o => o.Orderdate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderListViewModel
                {
                    Orderid = o.Orderid,
                    CustomerName = o.Customer.Firstname + " " + o.Customer.Lastname,
                    ProductName = o.Product.Productname,
                    Quantity = o.Quantity,
                    Totalprice = o.Totalprice,
                    Orderdate = o.Orderdate,
                    City = o.City,
                    Status = o.Status
                })
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.Search = search;

            return View(orders);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var customers = await _customerService.GetAllAsync();
            ViewBag.Customers = new SelectList(
                customers.Select(c => new
                {
                    c.Customerid,
                    FullName = c.Firstname + " " + c.Lastname
                }),
                "Customerid",
                "FullName"
            );
            ViewBag.Products = new SelectList(await _productService.GetAllAsync(), "Productid", "Productname");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            if (!ModelState.IsValid)
                return View(order);

            var customer = order.Customerid.HasValue
                ? await _customerService.GetByIdAsync(order.Customerid.Value)
                : null;

            var product = order.Productid.HasValue
                ? await _productService.GetByIdAsync(order.Productid.Value)
                : null;

            await _orderService.InsertAsync(order);

            string customerName = customer != null ? $"{customer.Firstname} {customer.Lastname}" : $"#{order.Customerid}";
            string productName = product != null ? product.Productname : $"#{order.Productid}";

            string detail = $"Yeni sipariş eklendi (Müşteri: {customerName}, Ürün: {productName}).";

            await LoggerHelper.LogAsync(_context, _httpContextAccessor, "Create", nameof(Order), detail);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            var customers = await _customerService.GetAllAsync();
            ViewBag.Customers = new SelectList(
                customers.Select(c => new
                {
                    c.Customerid,
                    FullName = c.Firstname + " " + c.Lastname
                }),
                "Customerid",
                "FullName"
            );
            ViewBag.Products = new SelectList(await _productService.GetAllAsync(), "Productid", "Productname");
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Order order)
        {
            if (!ModelState.IsValid)
                return View(order);

            var customer = order.Customerid.HasValue
                ? await _customerService.GetByIdAsync(order.Customerid.Value)
                : null;

            var product = order.Productid.HasValue
                ? await _productService.GetByIdAsync(order.Productid.Value)
                : null;

            await _orderService.UpdateAsync(order);

            string customerName = customer != null ? $"{customer.Firstname} {customer.Lastname}" : $"#{order.Customerid}";
            string productName = product != null ? product.Productname : $"#{order.Productid}";

            string detail = $"Sipariş güncellendi (Müşteri: {customerName}, Ürün: {productName}).";

            await LoggerHelper.LogAsync(_context, _httpContextAccessor, "Update", nameof(Order), detail);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _orderService.GetByIdAsync(id);

            var customer = deleted?.Customerid != null
                ? await _customerService.GetByIdAsync(deleted.Customerid.Value)
                : null;

            var product = deleted?.Productid != null
                ? await _productService.GetByIdAsync(deleted.Productid.Value)
                : null;

            await _orderService.DeleteAsync(id);

            string customerName = customer != null ? $"{customer.Firstname} {customer.Lastname}" : $"#{deleted?.Customerid}";
            string productName = product != null ? product.Productname : $"#{deleted?.Productid}";

            string detail = $"Sipariş silindi (Müşteri: {customerName}, Ürün: {productName}).";

            await LoggerHelper.LogAsync(_context, _httpContextAccessor, "Delete", nameof(Order), detail);

            return RedirectToAction("Index");
        }
    }
}
