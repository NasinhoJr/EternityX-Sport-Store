using EternityX.Data;
using EternityX.Models;
using EternityX.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EternityX.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize] // Само влезли потребители имат достъп до количката
    public class ShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Показва количката на текущия потребител
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var items = await _context.ShoppingCarts
                .Include(c => c.Product)
                    .ThenInclude(p => p.Brand)
                .Include(c => c.Product)
                    .ThenInclude(p => p.Category)
                .Include(c => c.Product)
                    .ThenInclude(p => p.Sizes)
                .Where(c => c.ApplicationUserId == userId)
                .ToListAsync();

            // Записва общия брой артикули в сесията
            HttpContext.Session.SetInt32(
                SD.SessionCart,
                items.Sum(i => i.Count)
            );

            return View(items);
        }

        // Добавя продукт в количката
        [HttpPost]
        [ValidateAntiForgeryToken] // Защита срещу невалидни заявки
        public async Task<IActionResult> Add(int productId, string size)
        {
            // Размерът е задължителен
            if (string.IsNullOrWhiteSpace(size))
                return Json(new { success = false });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Взима наличността за конкретния размер
            var productSize = await _context.ProductSizes
                .FirstOrDefaultAsync(ps => ps.ProductId == productId && ps.Size == size);

            // Проверява дали продуктът със същия размер вече е в количката
            var cartItem = await _context.ShoppingCarts.FirstOrDefaultAsync(c =>
                c.ProductId == productId &&
                c.ApplicationUserId == userId &&
                c.Size == size
            );

            if (cartItem == null)
            {
                // Ако го няма - създава нов запис
                cartItem = new ShoppingCart
                {
                    ProductId = productId,
                    ApplicationUserId = userId,
                    Size = size,
                    Count = 1
                };
                _context.ShoppingCarts.Add(cartItem);
            }
            else
            {
                // Не позволява да се надвиши наличността за този размер
                if (cartItem.Count >= productSize.Quantity)
                {
                    return Json(new
                    {
                        success = false,
                        message = $"Можеш да добавиш най-много {productSize.Quantity} бр. от размер {size}."
                    });
                }

                // Ако има наличност, увеличава количеството
                cartItem.Count++;
            }

            await _context.SaveChangesAsync();

            // Връща броя различни продукти в количката
            var cartCount = await _context.ShoppingCarts
                .Where(c => c.ApplicationUserId == userId)
                .CountAsync();

            return Json(new { success = true, cartCount });
        }

        // Обновява количеството на продукт в количката
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int id, int count)
        {
            if (count < 1) count = 1;

            var cartItem = await _context.ShoppingCarts
                .Include(c => c.Product)
                    .ThenInclude(p => p.Sizes)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cartItem == null)
                return RedirectToAction(nameof(Index));

            // Проверява наличността за конкретния размер
            var size = cartItem.Product.Sizes
                .FirstOrDefault(s => s.Size == cartItem.Size);

            if (size == null || count > size.Quantity)
                return RedirectToAction(nameof(Index));

            cartItem.Count = count;
            await _context.SaveChangesAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Обновява общия брой артикули в сесията
            HttpContext.Session.SetInt32(
                SD.SessionCart,
                await _context.ShoppingCarts
                    .Where(c => c.ApplicationUserId == userId)
                    .SumAsync(c => c.Count)
            );

            return RedirectToAction(nameof(Index));
        }

        // Премахва един продукт от количката
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _context.ShoppingCarts.FindAsync(id);
            if (item != null)
            {
                _context.ShoppingCarts.Remove(item);
                await _context.SaveChangesAsync();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Обновява сесията след премахване
            HttpContext.Session.SetInt32(
                SD.SessionCart,
                await _context.ShoppingCarts
                    .Where(c => c.ApplicationUserId == userId)
                    .SumAsync(c => c.Count)
            );

            return RedirectToAction(nameof(Index));
        }

        // Изчиства цялата количка
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Clear()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var items = _context.ShoppingCarts
                .Where(c => c.ApplicationUserId == userId);

            _context.ShoppingCarts.RemoveRange(items);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetInt32(SD.SessionCart, 0);
            return RedirectToAction(nameof(Index));
        }

        // Създава поръчка от продуктите в количката
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(
            string Name,
            string StreetAddress,
            string City,
            string State,
            string PostalCode)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Името е задължително
            if (string.IsNullOrWhiteSpace(Name))
                return RedirectToAction(nameof(Index));

            // Ако липсва град, опитва да го вземе от адреса
            if (string.IsNullOrWhiteSpace(City))
            {
                if (!string.IsNullOrWhiteSpace(StreetAddress) &&
                    (StreetAddress.Contains("–") || StreetAddress.Contains("-")))
                {
                    City = StreetAddress
                        .Split(new[] { '–', '-' })[0]
                        .Trim();
                }
                else
                {
                    City = "Офис EternityX";
                }
            }

            // Ако няма адрес, задава офис по подразбиране
            if (string.IsNullOrWhiteSpace(StreetAddress))
                StreetAddress = "Офис EternityX";

            State ??= "-";
            PostalCode ??= "-";

            var cartItems = await _context.ShoppingCarts
                .Include(c => c.Product)
                .Where(c => c.ApplicationUserId == userId)
                .ToListAsync();

            // Ако количката е празна, връща обратно
            if (!cartItems.Any())
                return RedirectToAction(nameof(Index));

            // Създава основната поръчка
            var order = new Order
            {
                ApplicationUserId = userId,
                OrderDate = DateTime.Now,
                OrderTotal = cartItems.Sum(i => i.Product.Price * i.Count),
                Name = Name,
                StreetAddress = StreetAddress,
                City = City,
                State = State,
                PostalCode = PostalCode
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in cartItems)
            {
                // Взима продукта
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == item.ProductId);

                // Взима конкретния размер
                var productSize = await _context.ProductSizes
                    .FirstOrDefaultAsync(ps =>
                        ps.ProductId == item.ProductId &&
                        ps.Size == item.Size);

                // Проверка за достатъчна наличност
                if (product == null ||
                    product.Stock < item.Count ||
                    productSize == null ||
                    productSize.Quantity < item.Count)
                {
                    TempData["Error"] =
                        $"Недостатъчна наличност за {item.Product.Name} – размер {item.Size}";
                    return RedirectToAction(nameof(Index));
                }

                // Намалява наличността след поръчка
                product.Stock -= item.Count;
                productSize.Quantity -= item.Count;

                // Добавя продуктите в OrderItems
                _context.OrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Count = item.Count,
                    Price = item.Product.Price
                });
            }

            // Изчиства количката след успешна поръчка
            _context.ShoppingCarts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetInt32(SD.SessionCart, 0);
            TempData["Success"] = "Поръчката е приета успешно!";

            return RedirectToAction(nameof(Index));
        }
    }
}