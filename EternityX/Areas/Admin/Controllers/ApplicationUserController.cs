using EternityX.Data;
using EternityX.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EternityX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Администратор")] // Достъп има само потребител с роля "Администратор"
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationUserController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Admin/ApplicationUser
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            // Взима всички потребители, без текущо логнатия
            // Подрежда ги по потребителско име
            var users = await _userManager.Users
                .Where(u => currentUser == null || u.Id != currentUser.Id)
                .OrderBy(u => u.UserName)
                .ToListAsync();

            return View(users);
        }

        // GET: Admin/ApplicationUser/Details/5
        public async Task<IActionResult> Details(string id)
        {
            // Проверка дали id е подадено
            if (string.IsNullOrWhiteSpace(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Подаваме ролите към изгледа
            ViewData["Roles"] = await _userManager.GetRolesAsync(user);
            return View(user);
        }

        // GET: Admin/ApplicationUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ApplicationUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Защита срещу CSRF атаки
        public async Task<IActionResult> Create(ApplicationUser model, string password)
        {
            // Проверка дали е въведена парола
            if (string.IsNullOrWhiteSpace(password))
                ModelState.AddModelError("Password", "Паролата е задължителна.");

            if (!ModelState.IsValid)
                return View(model);

            // Проверка за валиден имейл формат
            if (!new EmailAddressAttribute().IsValid(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Невалиден имейл формат.");
            }

            // Проверка дали потребителското име вече съществува
            if (!string.IsNullOrWhiteSpace(model.UserName) &&
                await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError(nameof(model.UserName), "Това потребителско име вече съществува.");
            }

            // Проверка дали имейлът вече съществува
            if (!string.IsNullOrWhiteSpace(model.Email) &&
                await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError(nameof(model.Email), "Този имейл вече съществува.");
            }

            // Проверка на телефонния номер - допуска + и между 10 и 15 цифри
            if (!string.IsNullOrWhiteSpace(model.PhoneNumber) && !Regex.IsMatch(model.PhoneNumber, @"^\+?\d{10,15}$"))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Невалиден телефонен номер.");
            }

            // Ако има грешки след всички проверки, връща формата
            if (!ModelState.IsValid)
                return View(model);

            // Създаване на нов потребител
            var user = new ApplicationUser
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                StreetAddress = model.StreetAddress,
                City = model.City,
                State = model.State,
                PostalCode = model.PostalCode,
                EmailConfirmed = true // Имейлът се маркира като потвърден автоматично
            };

            // Създаване на потребителя в базата с подадената парола
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                // Показва всички грешки от Identity
                foreach (var err in result.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);

                return View(model);
            }

            // Ако няма роля "Customer", я създава
            if (!await _roleManager.RoleExistsAsync("Customer"))
                await _roleManager.CreateAsync(new IdentityRole("Customer"));

            // Добавя новия потребител към роля "Customer"
            await _userManager.AddToRoleAsync(user, "Customer");

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/ApplicationUser/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            // Проверка дали id е подадено
            if (string.IsNullOrWhiteSpace(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: Admin/ApplicationUser/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser model)
        {
            // Проверка дали id от URL съвпада с id от модела
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Проверка за валиден имейл формат
            if (!new EmailAddressAttribute().IsValid(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Невалиден имейл формат.");
            }

            // Проверка дали новият имейл вече се използва от друг потребител
            if (await _userManager.FindByEmailAsync(model.Email) != null && model.Email != user.Email)
            {
                ModelState.AddModelError(nameof(model.Email), "Този имейл вече съществува.");
            }

            // Проверка дали новото потребителско име вече се използва от друг потребител
            if (await _userManager.FindByNameAsync(model.UserName) != null && model.UserName != user.UserName)
            {
                ModelState.AddModelError(nameof(model.UserName), "Това потребителско име вече съществува.");
            }

            // Проверка на телефонния номер
            if (!string.IsNullOrWhiteSpace(model.PhoneNumber) && !Regex.IsMatch(model.PhoneNumber, @"^\+?\d{10,15}$"))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Невалиден телефонен номер.");
            }

            if (!ModelState.IsValid)
                return View(model);

            // Обновяване на данните на потребителя
            user.Name = model.Name;
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;
            user.StreetAddress = model.StreetAddress;
            user.City = model.City;
            user.State = model.State;
            user.PostalCode = model.PostalCode;

            // Запис на промените
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/ApplicationUser/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            // Проверка дали id е подадено
            if (string.IsNullOrWhiteSpace(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Защита: администраторът да не може да изтрие собствения си акаунт
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null && currentUser.Id == user.Id)
            {
                TempData["Error"] = "Не можеш да изтриеш собствения си акаунт.";
                return RedirectToAction(nameof(Index));
            }

            // Изтриване на потребителя
            var result = await _userManager.DeleteAsync(user);

            // Ако има грешка, я записва в TempData
            if (!result.Succeeded)
                TempData["Error"] = string.Join(" | ", result.Errors.Select(e => e.Description));

            return RedirectToAction(nameof(Index));
        }
    }
}