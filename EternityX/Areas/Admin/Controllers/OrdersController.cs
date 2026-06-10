using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EternityX.Data;
using EternityX.Models;

namespace EternityX.Areas.Admin.Controllers
{
    [Area("Admin")] // Контролер за поръчките в Admin зоната
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Показва всички поръчки
        public async Task<IActionResult> Index()
        {
            // Зарежда поръчките заедно с потребителя към тях
            var applicationDbContext = _context.Orders.Include(o => o.ApplicationUser);
            var orders = await applicationDbContext.ToListAsync();

            // Добавя 5 евро. доставка само за показване
            foreach (var order in orders)
            {
                order.OrderTotal += 5;
            }

            return View(orders);
        }

        // Показва детайли за конкретна поръчка
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            // Добавя цена за доставка към крайната сума
            order.OrderTotal += 5;

            return View(order);
        }

        // Показва потвърждение преди изтриване
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            // Добавя доставка за правилно показване на сумата
            order.OrderTotal += 5;
            return View(order);
        }

        // Изтрива поръчката от базата
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // Защита срещу невалидни заявки
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Проверява дали поръчката съществува
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}