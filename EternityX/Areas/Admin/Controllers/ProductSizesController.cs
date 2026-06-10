using EternityX.Data;
using EternityX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EternityX.Areas.Admin.Controllers
{
    [Area("Admin")] // Контролер за размерите на продуктите в Admin зоната
    public class ProductSizesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductSizesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Показва всички размери заедно с продукта към тях
        public async Task<IActionResult> Index()
        {
            var sizes = await _context.ProductSizes
                .Include(ps => ps.Product)
                .ToListAsync();

            return View(sizes);
        }

        // Зарежда формата за създаване
        public IActionResult Create()
        {
            ViewBag.ProductId = new SelectList(
                _context.Products,
                "Id",    // това се записва
                "Name"   // това се показва
            );

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Защита срещу невалидни заявки
        public async Task<IActionResult> Create(ProductSize model)
        {
            // Проверка дали е избран продукт
            if (model.ProductId == 0)
            {
                ModelState.AddModelError("ProductId", "Моля, изберете продукт");
            }

            if (!ModelState.IsValid)
            {
                // Презарежда dropdown-а и запазва избора
                ViewBag.ProductId = new SelectList(
                    _context.Products,
                    "Id",
                    "Name",
                    model.ProductId
                );

                return View(model);
            }

            _context.ProductSizes.Add(model);
            await _context.SaveChangesAsync();

            // Обновява общата наличност на продукта
            await UpdateProductStock(model.ProductId);

            return RedirectToAction(nameof(Index));
        }

        // Зарежда формата за редакция
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.ProductSizes
                .Include(ps => ps.Product)
                .FirstOrDefaultAsync(ps => ps.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductSize model)
        {
            var entity = await _context.ProductSizes.FindAsync(model.Id);
            if (entity == null)
                return NotFound();

            // Обновяват се само нужните полета
            entity.Size = model.Size;
            entity.Quantity = model.Quantity ?? 0;

            await _context.SaveChangesAsync();

            // След редакция се обновява складовата наличност
            await UpdateProductStock(entity.ProductId);

            return RedirectToAction(nameof(Index));
        }

        // Показва детайли за конкретен размер
        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.ProductSizes
                .Include(ps => ps.Product)
                .FirstOrDefaultAsync(ps => ps.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        // Показва потвърждение преди изтриване
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.ProductSizes
                .Include(ps => ps.Product)
                .FirstOrDefaultAsync(ps => ps.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _context.ProductSizes.FindAsync(id);
            if (entity == null)
                return NotFound();

            int productId = entity.ProductId;

            _context.ProductSizes.Remove(entity);
            await _context.SaveChangesAsync();

            // След изтриване също се обновява общият склад
            await UpdateProductStock(productId);

            return RedirectToAction(nameof(Index));
        }

        // Пресмята общата наличност на продукта от всички размери
        private async Task UpdateProductStock(int productId)
        {
            var total = await _context.ProductSizes
                .Where(ps => ps.ProductId == productId)
                .SumAsync(ps => ps.Quantity ?? 0); // ако Quantity е null, приема 0

            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Stock = total;
                await _context.SaveChangesAsync();
            }
        }
    }
}