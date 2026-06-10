using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EternityX.Data;
using EternityX.Models;
using Microsoft.AspNetCore.Hosting;

namespace EternityX.Areas.Admin.Controllers
{
    [Area("Admin")] // Контролер за продуктите в Admin зоната
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env; // Дава достъп до wwwroot за работа с файлове

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Показва всички продукти с тяхната категория и марка
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .ToListAsync();

            return View(products);
        }

        // Показва детайли за конкретен продукт
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // Зарежда формата за създаване
        public IActionResult Create()
        {
            LoadDropdowns(); // Зарежда категориите и марките за падащите менюта
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Защита срещу невалидни заявки
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns(product);
                return View(product);
            }

            // Ако има качен файл, той е с приоритет пред URL
            if (product.PictureFile != null)
            {
                var saved = await SavePictureAsync(product.PictureFile);
                if (saved != null)
                    product.Picture = saved;
            }
            // Ако няма файл, но има URL - записва URL адреса
            else if (!string.IsNullOrWhiteSpace(product.PictureUrl))
            {
                product.Picture = product.PictureUrl;
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Зарежда формата за редакция
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            LoadDropdowns(product);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns(product);
                return View(product);
            }

            // Взима стария запис от базата, за да не се загубят стари данни
            var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (dbProduct == null) return NotFound();

            // Обновява основните полета
            dbProduct.Name = product.Name;
            dbProduct.Description = product.Description;
            dbProduct.Price = product.Price;
            dbProduct.Stock = product.Stock;
            dbProduct.Color = product.Color;
            dbProduct.CategoryId = product.CategoryId;
            dbProduct.BrandId = product.BrandId;

            // Ако има нов качен файл - изтрива старата локална снимка и записва новата
            if (product.PictureFile != null)
            {
                DeleteLocalPictureIfAny(dbProduct.Picture);

                var saved = await SavePictureAsync(product.PictureFile);
                if (saved != null)
                    dbProduct.Picture = saved;
            }
            // Ако е подаден URL - записва URL вместо локална снимка
            else if (!string.IsNullOrWhiteSpace(product.PictureUrl))
            {
                DeleteLocalPictureIfAny(dbProduct.Picture);
                dbProduct.Picture = product.PictureUrl;
            }
            // Ако няма нито файл, нито URL - старата снимка се запазва

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Показва потвърждение за изтриване
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                // Ако има локална снимка, изтрива и файла от папката
                DeleteLocalPictureIfAny(product.Picture);

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Зарежда категориите и марките в dropdown списъците
        private void LoadDropdowns(Product product = null)
        {
            ViewBag.CategoryId = new SelectList(
                _context.Categories,
                "Id",
                "Name",
                product?.CategoryId
            );

            ViewBag.BrandId = new SelectList(
                _context.Brands,
                "Id",
                "Name",
                product?.BrandId
            );
        }

        // Записва качената снимка в wwwroot/images/products
        private async Task<string?> SavePictureAsync(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            var uploads = Path.Combine(_env.WebRootPath, "images", "products");
            Directory.CreateDirectory(uploads); // Създава папката, ако не съществува

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            // Позволява само определени файлови формати
            if (!allowed.Contains(ext)) return null;

            var fileName = $"{Guid.NewGuid()}{ext}"; // Генерира уникално име
            var path = Path.Combine(uploads, fileName);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName; // Записва се в полето Picture
        }

        // Изтрива локална снимка, ако стойността не е външен URL
        private void DeleteLocalPictureIfAny(string? pictureValue)
        {
            if (string.IsNullOrWhiteSpace(pictureValue)) return;

            // Ако е URL, не се трие файл
            if (pictureValue.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                return;

            var path = Path.Combine(_env.WebRootPath, "images", "products", pictureValue);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
    }
}