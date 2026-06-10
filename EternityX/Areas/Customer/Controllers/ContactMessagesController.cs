using EternityX.Data;
using EternityX.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EternityX.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ContactMessagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactMessagesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(string subject, string message)
        {
            var user = await _userManager.GetUserAsync(User);

            var contact = new ContactMessage
            {
                UserId = user.Id,
                Subject = subject,
                Message = message
            };

            _context.ContactMessages.Add(contact);
            await _context.SaveChangesAsync();

            TempData["success"] = "Съобщението е изпратено успешно.";
            return RedirectToAction(nameof(Index));
        }
    }
}
