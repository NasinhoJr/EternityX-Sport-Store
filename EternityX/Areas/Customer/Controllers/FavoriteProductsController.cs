using EternityX.Data;
using EternityX.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EternityX.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize] // Само влезли потребители имат достъп
    public class FavoriteProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoriteProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Показва любимите продукти само на текущия потребител
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favoriteProducts = await _context.FavoriteProduct
                .Where(f => f.ApplicationUserId == userId)
                .Include(f => f.ApplicationUser)
                .Include(f => f.Product)
                .ToListAsync();

            return View(favoriteProducts);
        }

        // Показва детайли само ако записът е на текущия потребител
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favoriteProduct = await _context.FavoriteProduct
                .Include(f => f.ApplicationUser)
                .Include(f => f.Product)
                .FirstOrDefaultAsync(m => m.Id == id && m.ApplicationUserId == userId);

            if (favoriteProduct == null)
            {
                return NotFound();
            }

            return View(favoriteProduct);
        }

        // Зарежда форма за ръчно добавяне в любими
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // Добавя продукт в любими
        [HttpPost]
        [ValidateAntiForgeryToken] // Защита срещу невалидни заявки
        public async Task<IActionResult> Create(int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Не сте автентикиран." });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Проверява дали продуктът вече е добавен
            if (await IsProductInFavorites(userId, productId))
            {
                return Json(new { success = false, message = "Продуктът вече е в любими." });
            }

            var favoriteProduct = new FavoriteProduct
            {
                ApplicationUserId = userId,
                ProductId = productId
            };

            _context.FavoriteProduct.Add(favoriteProduct);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Продуктът беше добавен в любими." });
        }

        // Проверява дали продуктът вече е в любими за този потребител
        private async Task<bool> IsProductInFavorites(string userId, int productId)
        {
            return await _context.FavoriteProduct
                .AnyAsync(f => f.ApplicationUserId == userId && f.ProductId == productId);
        }

        // Показва потвърждение за премахване от любими
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favoriteProduct = await _context.FavoriteProduct
                .Include(f => f.ApplicationUser)
                .Include(f => f.Product)
                .FirstOrDefaultAsync(m => m.Id == id && m.ApplicationUserId == userId);

            if (favoriteProduct == null)
            {
                return NotFound();
            }

            return View(favoriteProduct);
        }

        // Премахва продукта от любими само за текущия потребител
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favoriteProduct = await _context.FavoriteProduct
                .FirstOrDefaultAsync(f => f.Id == id && f.ApplicationUserId == userId);

            if (favoriteProduct != null)
            {
                _context.FavoriteProduct.Remove(favoriteProduct);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Проверява дали записът съществува
        private bool FavoriteProductExists(int id)
        {
            return _context.FavoriteProduct.Any(e => e.Id == id);
        }
    }
}