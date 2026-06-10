using EternityX.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternityX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Администратор")]
    public class ContactMessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactMessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Списък със съобщения
        public async Task<IActionResult> Index()
        {
            var messages = await _context.ContactMessages
                .Include(m => m.User)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            return View(messages);
        }

        // Детайли за едно съобщение
        public async Task<IActionResult> Details(int id)
        {
            var message = await _context.ContactMessages
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (message == null)
                return NotFound();

            return View(message);
        }
    }
}

