using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganikPazar.Context;
using OrganikPazar.Entities;
using OrganikPazar.Helpers;

namespace OrganikPazar.Controllers
{
    public class LogController : Controller
    {
        private readonly OrganikPazarContext _context;

        public LogController(OrganikPazarContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _context.Logs
                .OrderByDescending(x => x.Actiondate)
                .Take(100)
                .AsNoTracking() 
                .ToListAsync();

            return View(logs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var log = await _context.Logs.AsNoTracking().FirstOrDefaultAsync(x => x.Logid == id);
            if (log == null)
                return NotFound();

            return View(log);
        }

        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            _context.Logs.RemoveRange(_context.Logs);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Tüm log kayıtları başarıyla silindi.";
            return RedirectToAction("Index");
        }
    }
}
