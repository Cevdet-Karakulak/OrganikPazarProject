using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganikPazar.Context;
using OrganikPazar.Dtos.CustomerDtos;
using OrganikPazar.Entities;
using OrganikPazar.Helpers;
using OrganikPazar.Services.Interfaces;

namespace OrganikPazar.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IGenericService<Customer> _customerService;
        private readonly OrganikPazarContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerController(
            IGenericService<Customer> customerService,
            OrganikPazarContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _customerService = customerService;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(string search = "", int page = 1, int pageSize = 25)
        {
            var query = _customerService.GetQueryable().AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                var lowered = search.ToLower();
                query = query.Where(c =>
                    (c.Firstname + " " + c.Lastname).ToLower().Contains(lowered) ||
                    c.Email.ToLower().Contains(lowered) ||
                    (c.City ?? "").ToLower().Contains(lowered));
            }

            var totalCount = await query.CountAsync();

            var customers = await query
                .OrderBy(c => c.Firstname)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ResultCustomerDto
                {
                    Customerid = c.Customerid,
                    Fullname = c.Firstname + " " + c.Lastname,
                    Email = c.Email,
                    Phone = c.Phone,
                    City = c.City,
                    Address = c.Address,
                    Registerdate = c.Registerdate
                })
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.Search = search;

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            if (!customer.Registerdate.HasValue)
                customer.Registerdate = DateTime.Now;

            await _customerService.InsertAsync(customer);
            await LoggerHelper.LogAsync(_context, _httpContextAccessor, "Create", nameof(Customer), $"{customer.Firstname} {customer.Lastname} müşterisi eklendi.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            var existingCustomer = await _customerService.GetByIdAsync(customer.Customerid);
            if (existingCustomer == null)
                return NotFound();

            existingCustomer.Firstname = customer.Firstname;
            existingCustomer.Lastname = customer.Lastname;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.City = customer.City;
            existingCustomer.Address = customer.Address;

            await _customerService.UpdateAsync(existingCustomer);
            await LoggerHelper.LogAsync(_context, _httpContextAccessor, "Update", nameof(Customer), $"{customer.Firstname} {customer.Lastname} müşterisi güncellendi.");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _customerService.GetByIdAsync(id);
            await _customerService.DeleteAsync(id);
            await LoggerHelper.LogAsync(_context, _httpContextAccessor, "Delete", nameof(Customer), $"{deleted?.Firstname} {deleted?.Lastname} müşterisi silindi.");
            return RedirectToAction("Index");
        }
    }
}
