using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EternityX.Data;
using EternityX.Models;

namespace EternityX.Areas.Customer.Controllers
{
    [Area("Customer")] // Контролерът е в Customer зоната
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Показва списък с продукти + филтриране, търсене и сортиране
        public async Task<IActionResult> Index(
            string search,
            int? categoryId,
            int? brandId,
            decimal? minPrice,
            decimal? maxPrice,
            string stock,
            string sort)
        {
            // Ако се въведе отрицателна цена, я правим 0
            if (minPrice < 0) minPrice = 0;
            if (maxPrice < 0) maxPrice = 0;

            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .AsQueryable();

            // Търсене по име на продукт
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            // Филтър по категория
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            // Филтър по марка
            if (brandId.HasValue)
            {
                query = query.Where(p => p.BrandId == brandId);
            }

            // Филтър по минимална цена
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice);
            }

            // Филтър по максимална цена
            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice);
            }

            // Филтър по наличност
            if (stock == "in")
            {
                query = query.Where(p => p.Stock > 5);
            }
            else if (stock == "low")
            {
                query = query.Where(p => p.Stock > 0 && p.Stock <= 5);
            }
            else if (stock == "out")
            {
                query = query.Where(p => p.Stock == 0);
            }

            // Сортиране според избрания вариант
            query = sort switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "name" => query.OrderBy(p => p.Name),
                "stock" => query.OrderByDescending(p => p.Stock),
                _ => query.OrderByDescending(p => p.Id) // по подразбиране: най-новите
            };

            // Данни за филтрите в изгледа
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Brands = await _context.Brands.ToListAsync();

            return View(await query.ToListAsync());
        }

        // Показва детайли за конкретен продукт
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Sizes) // Зарежда и размерите на продукта
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}